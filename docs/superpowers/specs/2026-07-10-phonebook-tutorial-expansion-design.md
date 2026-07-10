# PhoneBook Educational Tutorial Design

## Goal

Replace the short, reference-style `PhoneBook/phonebook_tutorial.md` with a thorough learning guide for readers who know basic C# but need help reading a real .NET console application. The guide must explain the running flow, every authored and generated project file, and the reason each part exists—not merely list filenames or features.

## Audience and teaching style

- Assume the reader can follow basic classes, methods, variables, and `if` statements.
- Explain less familiar C# and .NET terms at first use in short **C# note** callouts: `partial`, `sealed`, `record`, nullable `?`, attributes, `Func<T>`, lambda expressions, LINQ, `using var`, `DbSet`, `DbContext`, `Include`, `AsNoTracking`, navigation properties, and migrations.
- Teach the finished application's behavior. Do not turn this into a long build-from-scratch course or reproduce large source files.
- Use concrete method paths and small excerpts only when they clarify a decision. Each explanation must answer **what it does**, **why it exists**, **how it participates in the flow**, and **which files it connects to**.
- Distinguish current behavior from limitations or implementation quirks. Do not present a limitation as a feature.

## Scope

- Modify `PhoneBook/phonebook_tutorial.md` only during implementation.
- Use `PhoneBook/` and `PhoneBook.Tests/` as the source of truth, with `.gitignore`, `README.md`, `main-menu.jpg`, and `contacts-table.jpg` documented as repository assets.
- Name every non-build project file once: application code, solution/project files, tests, migrations, generated migration metadata, configuration, and repository assets.
- Explain runtime-created or ignored artifacts separately: `config.json`, `PhoneBook.db`, `.vs`, `bin`, and `obj`.
- Preserve credential safety: configuration examples must be placeholders and no credentials, private phone numbers, or real provider claims may appear.
- Do not change runtime code, dependencies, tests, database contents, screenshots, or ignore rules as part of the tutorial rewrite.

## Tutorial structure

1. **Overview and learning map** — current capabilities, what the reader will learn, and how to use the guide.
2. **Run, build, test, and working directory** — explicit Windows-compatible commands for the app, solution, and tests. Explain that `PhoneBook.db` and `config.json` are resolved from the process working directory.
3. **Architecture at a glance** — a Mermaid component flow from `Program.cs` through UI, validation, services, EF Core, SQLite, configuration, SMTP, and Twilio.
4. **Startup, step by step** — trace `Program.cs`: migration application, manual service construction, UI construction, and `Run()`. Explain why the project uses simple manual composition rather than a dependency-injection container.
5. **Data model and persistence** — explain `Contact`, `Category`, `PhoneBookContext`, annotations, nullable foreign keys, navigation properties, seed data, and the entity relationship diagram.
6. **Contact vertical slice** — trace view, add, update, delete, and search from a menu choice through `UserInterface`, validation, `ContactService`, short-lived contexts, and SQLite. Make `Run -> AddContact -> Validation -> ContactService.AddContact -> SaveChanges` a concrete worked example, then summarize its siblings.
7. **Category vertical slice** — explain the nested category menu, normalization, case-insensitive duplicate prevention, the unique database index, optional assignment, loaded contact counts, and unassignment before deletion.
8. **Email and SMS vertical slices** — trace first-use configuration, recipient choice, service validation, `OperationResult`, and external-provider calls. Explain why normal contact phone validation differs from E.164 and why some stored phone numbers cannot be sent directly as SMS.
9. **Configuration and secret handling** — explain `AppConfig`, `ConfigManager`, `IsConfigured`, plaintext local `config.json`, ignored secrets, malformed-file behavior, and how incomplete non-null configuration produces a service failure rather than reopening setup.
10. **Database history and migrations** — use a Mermaid timeline for `InitialCreate`, `AddCategories`, and `AddUniqueCategoryName`; explain `Up`/`Down`, authored migration operations, Designer files, and the model snapshot. State which migration files are generated and must not be hand-edited.
11. **Tests: confidence and limits** — explain the test project, xUnit, `TestDbContextFactory`, shared GUID-named in-memory stores, each test class, and what the tests do not prove (SQLite behavior, real provider delivery, UI, and configuration-file I/O).
12. **Complete file reference** — grouped, scannable file cards/tables. Every item receives `What`, `Why`, `How/Connects to`; generated files are grouped only where their shared role makes an individual deep dive redundant.
13. **Troubleshooting and next reading** — SDK, migration/database, working-directory, config, SMTP, and Twilio problems, plus a suggested source-reading order.

## Diagrams

Use compact Mermaid diagrams that earn their space:

1. **Architecture flowchart** — `Program.cs` -> services and UI; UI -> validation/configuration; CRUD services -> `PhoneBookContext` -> SQLite; communication services -> SMTP/Twilio.
2. **Contact sequence diagram** — add-contact path through prompt validation, optional category lookup, entity creation, `Add`, and `SaveChanges`.
3. **Entity relationship diagram** — one optional `Category` to many `Contact` rows through nullable `CategoryId`.
4. **Migration timeline** — the ordered three migrations and the schema change made by each.

Keep labels short, avoid duplicating prose inside diagrams, and introduce each diagram with the question it answers.

## File-reference rules

- Explain `PhoneBook.slnx`, `PhoneBook.csproj`, and `PhoneBook.Tests.csproj`; the solution must not be omitted.
- Cover `Program.cs`, UI, models, context, validation, configuration classes, each service, and `OperationResult` individually.
- Cover all three authored migration files individually. Explain both Designer files and `PhoneBookContextModelSnapshot.cs` as generated EF Core metadata; note that the third migration uses explicit attributes and has no corresponding Designer file in this repository.
- Cover the test database factory and every test class, accurately describing their current assertions rather than calling the suite "full CRUD" coverage.
- Explain `.gitignore`, `README.md`, screenshots, and ignored/runtime artifacts. Do not describe Git internals or unrelated workspace folders.

## Accuracy requirements

- Describe contact search as SQL `LIKE` substring matching, not fuzzy matching.
- State that menu validation protects interactive entry, but direct callers of `ContactService` can bypass it.
- State that data annotations shape EF Core model metadata; they are not automatically executed as all UI validation during `SaveChanges`.
- Explain that read methods use `Include` and `AsNoTracking`, while update methods load a tracked entity by ID and copy values into it.
- Explain that CRUD services create/dispose a context per operation and use an optional `Func<PhoneBookContext>` only to support tests.
- Explain `OperationResult` precisely: it carries success/failure and a message; the UI prints its own success text and displays the returned message on failure.
- Do not promise email or SMS delivery. Correct settings only allow a provider request.

## Verification

- Compare the complete file reference against `rg --files --hidden` with build and Git directories excluded.
- Confirm every diagram has valid Mermaid fences and a short explanatory lead-in.
- Confirm each first-use keyword appears with a short plain-language explanation.
- Check headings, links, tables, code fences, screenshot paths, and commands in a Markdown renderer.
- Search for stale claims such as `fuzzy`, `full CRUD`, `Latest`, `TODO`, `TBD`, and `file://`.
- Run `dotnet test PhoneBook.Tests/PhoneBook.Tests.csproj` without modifying source code.
