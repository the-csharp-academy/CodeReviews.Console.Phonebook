# PhoneBook Educational Tutorial Design

## Goal

Replace the short, reference-style `PhoneBook/phonebook_tutorial.md` with a thorough, **EF Core-centered** learning guide for readers who know basic C# but need help reading a real .NET console application. The guide must explain the running flow, every authored and generated project file, and the reason each part exists—not merely list filenames or features.

EF Core is the tutorial's main subject. The console UI, validation, configuration, email, and SMS code exist chiefly to show where real application code creates, queries, changes, and persists EF Core entities.

## Audience and teaching style

- Assume the reader can follow basic classes, methods, variables, and `if` statements.
- Explain less familiar C# and .NET terms at first use in short **C# note** callouts: `partial`, `sealed`, `record`, nullable `?`, attributes, `Func<T>`, lambda expressions, LINQ, and `using var`.
- Build an EF Core vocabulary progressively: ORM, entity, model, `DbContext`, `DbSet`, connection string, query, LINQ translation, `Include`, `AsNoTracking`, change tracking, `SaveChanges`, migration, model snapshot, seed data, foreign key, and navigation property. Define a term before relying on it.
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
- Keep non-EF subjects proportional: describe them accurately and connect them to the data flow, but do not let provider-specific SMTP or Twilio details displace EF Core teaching.

## Tutorial structure

1. **Overview and EF learning map** — current capabilities, the EF Core concepts the reader will learn, and a recommended reading order.
2. **Run, build, test, and working directory** — explicit Windows-compatible commands for the app, solution, and tests. Explain that `PhoneBook.db` and `config.json` are resolved from the process working directory.
3. **EF Core mental model** — define ORM, entity, model, `DbContext`, `DbSet`, and SQLite before introducing the project's files. Explain the division between C# objects in memory and rows in a database.
4. **Architecture and startup** — a compact component diagram, then trace `Program.cs`: `Database.Migrate()`, manual service/UI construction, and `Run()`. Explain why migration runs before any user action and why this small project uses manual composition instead of a dependency-injection container.
5. **Model the database** — deeply explain `Contact.cs`, `Category.cs`, and `PhoneBookContext.cs`: conventions, annotations, `OnConfiguring`, the SQLite connection string, `DbSet` properties, seed data, nullable foreign keys, navigation properties, the unique category-name index, and the entity relationship diagram.
6. **EF Core query and tracking lifecycle** — make this the central chapter. Compare `GetAllContacts`/`GetAllCategories` (`Include`, `AsNoTracking`, ordering) with `UpdateContact`/`UpdateCategory` (`Find`, tracked entity, copying changed values, `SaveChanges`). Explain why a fresh context is created and disposed per service operation, and how the optional `Func<PhoneBookContext>` enables tests.
7. **Contact vertical slice** — trace view, add, update, delete, and search from a menu choice to EF Core. Make `Run -> AddContact -> Validation -> ContactService.AddContact -> DbSet.Add -> SaveChanges` the worked example; identify which validation is UI-only and which behavior EF enforces.
8. **Category relationship vertical slice** — explain optional assignment, `Include(category => category.Contacts)`, contact counts, normalization, case-insensitive duplicate prevention, the database unique index, and setting every related `CategoryId` to `null` before deletion.
9. **Schema evolution with migrations** — use a Mermaid timeline for `InitialCreate`, `AddCategories`, and `AddUniqueCategoryName`; explain `Up`/`Down`, authored migration operations, Designer files, and the model snapshot. State which files are generated and must not be hand-edited.
10. **Testing EF behavior and its limits** — explain xUnit, `TestDbContextFactory`, the shared GUID-named EF InMemory store, each test class, and why InMemory tests do not prove SQLite-specific behavior, migrations, or database constraints.
11. **UI, configuration, and communication as consumers** — concisely show how `UserInterface`, `Validation`, configuration classes, `EmailService`, `SmsService`, and `OperationResult` sit around the EF core. Explain first-use configuration, secrets, and E.164 only as needed to understand their interaction with contacts.
12. **Complete file reference** — grouped, scannable file cards/tables. Every item receives `What`, `Why`, `How/Connects to`; generated files are grouped only where their shared role makes an individual deep dive redundant.
13. **EF-focused troubleshooting and next reading** — SDK, context/database path, migrations, stale schema, tracking surprises, configuration, SMTP, and Twilio problems, plus a suggested source-reading order.

## Diagrams

Use compact Mermaid diagrams that earn their space:

1. **Architecture flowchart** — `Program.cs` -> services and UI; UI -> validation/configuration; CRUD services -> `PhoneBookContext` -> SQLite; communication services -> SMTP/Twilio.
2. **EF entity lifecycle sequence** — add-contact path through prompt validation, optional category lookup, entity creation, `DbSet.Add`, change tracking, and `SaveChanges`.
3. **Query versus update flow** — a compact comparison of `Include` + `AsNoTracking` reads and `Find` + tracked-entity updates.
4. **Entity relationship diagram** — one optional `Category` to many `Contact` rows through nullable `CategoryId`.
5. **Migration timeline** — the ordered three migrations and the schema change made by each.

Keep labels short, avoid duplicating prose inside diagrams, and introduce each diagram with the question it answers.

## File-reference rules

- Explain `PhoneBook.slnx`, `PhoneBook.csproj`, and `PhoneBook.Tests.csproj`; the solution must not be omitted.
- Cover `Program.cs`, UI, models, context, validation, configuration classes, each service, and `OperationResult` individually.
- Give `Contact.cs`, `Category.cs`, `PhoneBookContext.cs`, `ContactService.cs`, and `CategoryService.cs` the deepest explanations. Their cards must connect code directly to EF Core concepts and to the relevant diagram/vertical slice.
- Cover all three authored migration files individually. Explain both Designer files and `PhoneBookContextModelSnapshot.cs` as generated EF Core metadata; note that the third migration uses explicit attributes and has no corresponding Designer file in this repository.
- Cover the test database factory and every test class, accurately describing their current assertions rather than calling the suite "full CRUD" coverage.
- Explain `.gitignore`, `README.md`, screenshots, and ignored/runtime artifacts. Do not describe Git internals or unrelated workspace folders.

## Accuracy requirements

- Describe contact search as SQL `LIKE` substring matching, not fuzzy matching.
- State that menu validation protects interactive entry, but direct callers of `ContactService` can bypass it.
- State that data annotations shape EF Core model metadata; they are not automatically executed as all UI validation during `SaveChanges`.
- Explain that read methods use `Include` and `AsNoTracking`, while update methods load a tracked entity by ID and copy values into it.
- Explain that `DbSet.Add` marks a new entity for insertion and `SaveChanges` turns tracked additions, modifications, and deletions into database commands.
- Explain that `OnConfiguring` selects SQLite and that the context is the unit through which EF Core knows the model, connection, and tracked changes.
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
