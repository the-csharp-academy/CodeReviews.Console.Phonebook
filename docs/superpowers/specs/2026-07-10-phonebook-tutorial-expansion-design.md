# PhoneBook EF Core Construction Tutorial Design

## Goal

Rewrite `PhoneBook/phonebook_tutorial.md` as an EF Core-centered construction guide. It must lead a reader with basic C# from **Step 0** through the final application, explaining which file is introduced or changed at each step, why that dependency comes next, and how it contributes to the next EF Core capability.

This is a **logical dependency order**, not a claim about the repository's historical commit order. The current repository state cannot reliably prove when every source file was first created.

## Language

- Write all explanatory prose, headings, table labels, diagram labels, checkpoints, and troubleshooting guidance in Vietnamese.
- Preserve source-code text, file paths, package names, shell commands, class names, method names, property names, migration IDs, and JSON property names exactly as they appear in the repository.
- At first use, write a Vietnamese explanation followed by the English technical term in parentheses—for example, "ngữ cảnh cơ sở dữ liệu (`DbContext`)" and "theo dõi thay đổi (change tracking)". Later uses may use the C# or EF Core identifier directly.
- Do not create parallel English and Vietnamese paragraphs. A single Vietnamese narrative with English identifiers is the readable source of truth.

## Audience and teaching style

- Assume basic C#: classes, methods, variables, and `if` statements.
- Explain unfamiliar C# syntax at first use: `partial`, `sealed`, `record`, nullable `?`, attributes, `Func<T>`, lambda expressions, LINQ, and `using var`.
- Use Vietnamese phrasing that explains the concept before the English identifier. Avoid literal translation where a short plain-language explanation is clearer.
- Make EF Core the main thread. Define ORM, entity, model, `DbContext`, `DbSet`, connection string, query, `Include`, `AsNoTracking`, change tracking, `SaveChanges`, migration, model snapshot, seed data, foreign key, and navigation property before using them as shorthand.
- Every numbered step contains: **Goal**, **Files introduced or changed**, **Why now**, **What to add or understand**, **EF Core lesson**, **Connection to the next step**, and a short verification checkpoint.
- Use the final checked-in files as cumulative snapshots. A step may point to a class or method that now contains later features, but must identify the subsection relevant to that step rather than pretending the final file existed unchanged at the time.
- Keep non-EF subjects proportionate. UI, configuration, SMTP, and Twilio should clarify how callers reach the EF-backed model, not displace EF Core instruction.

## Scope

- Modify `PhoneBook/phonebook_tutorial.md` only during implementation.
- Treat `PhoneBook/`, `PhoneBook.Tests/`, `.gitignore`, `README.md`, `main-menu.jpg`, and `contacts-table.jpg` as the source of truth.
- Name every non-build application, test, migration, project, solution, and repository asset file once. Explain generated migration metadata after naming each generated file.
- Explain `config.json`, `PhoneBook.db`, `.vs`, `bin`, and `obj` as runtime/generated artifacts.
- Keep credentials and phone numbers as placeholders. Do not promise email or SMS delivery.
- Do not modify runtime code, dependencies, tests, screenshots, database files, or ignore rules.

## Step-by-step tutorial structure

### Step 0 — Create the shell

Introduce `PhoneBook.slnx` and `PhoneBook.csproj`: the solution, .NET 10 executable, nullable reference types, and EF Core/SQLite packages. Show restore/run/build/test commands and explain the working-directory location of `PhoneBook.db` and `config.json`.

### Step 1 — Create the first entity

Introduce `Models/Contact.cs`. Explain an entity, primary key convention, `[Required]`, `[MaxLength]`, optional email, and nullable `CategoryId` as a placeholder for a relationship introduced later. Distinguish EF model metadata from interactive validation.

### Step 2 — Add the context and first schema

Introduce `Data/PhoneBookContext.cs` with `DbSet<Contact>`, `UseSqlite("Data Source=PhoneBook.db")`, seed contacts, and the first model. Then introduce `Migrations/20260709081750_InitialCreate.cs` and its Designer file as the initial schema transition. Define `DbContext`, `DbSet`, connection string, seed data, migration, `Up`, and `Down`.

### Step 3 — Query and save contacts

Introduce the contact-focused portions of `Services/ContactService.cs`. Build from `GetAllContacts` to add, update, delete, and search. Teach LINQ, `Include`, `AsNoTracking`, `Find`, `Add`, `Remove`, `EF.Functions.Like`, change tracking, and `SaveChanges`. Explain the optional `Func<PhoneBookContext>` factory and `using var` when it first appears.

### Step 4 — Give the first EF flow a user interface

Introduce `Helpers/Validation.cs` and the contact-focused parts of `UI/UserInterface.cs`, then `Program.cs` as the composition root that calls `Database.Migrate()` and begins `Run()`. Trace one complete Add Contact path from prompt validation through `DbSet.Add` and `SaveChanges`. Explain that direct service callers can bypass UI validation.

### Step 5 — Test the first data layer

Introduce `PhoneBook.Tests/PhoneBook.Tests.csproj`, `Helpers/TestDbContextFactory.cs`, and `ContactServiceTests.cs`. Explain a derived context, the EF InMemory provider, GUID-named shared stores, and the context-factory test seam. State what the tests prove and why they do not prove SQLite behavior.

### Step 6 — Extend the model with categories

Introduce `Models/Category.cs`, the category-related changes in `Contact.cs` and `PhoneBookContext.cs`, `Services/CategoryService.cs`, and the category UI. Teach nullable foreign keys, navigation properties, `Include(category => category.Contacts)`, category assignment, and contact counts. Immediately follow those changes with `Migrations/20260709171645_AddCategories.cs` and its Designer file.

### Step 7 — Enforce category uniqueness and test relationships

Introduce `Migrations/20260710000000_AddUniqueCategoryName.cs`, the unique `Categories.Name` index, normalized case-insensitive service checks, and `CategoryServiceTests.cs`. Explain the difference between application-level duplicate messaging and database-level constraints. Trace category deletion setting each related `CategoryId` to `null` before saving/removing the category.

### Step 8 — Add supporting features around the data layer

Introduce `Configuration/AppConfig.cs`, `Configuration/ConfigManager.cs`, `Services/EmailService.cs`, `Services/SmsService.cs`, `Services/OperationResult.cs`, `CommunicationServiceTests.cs`, and the remaining UI paths. Explain configuration/secrets, E.164 versus general contact phone validation, `sealed`, `record`, and provider failure results. Keep the focus on how the features select or use persisted contacts.

### Step 9 — Verify, evolve, and navigate the finished project

Introduce `ValidationTests.cs`, `MigrationTests.cs`, `PhoneBookContextModelSnapshot.cs`, future migration commands, troubleshooting, source-reading order, screenshots, `README.md`, `.gitignore`, and the complete file reference. Explain generated metadata, test limits, runtime artifacts, and next EF Core experiments.

## Diagrams

Use five compact Mermaid diagrams, introduced at the step where each answers a new question:

1. **Dependency roadmap** at Step 0: Steps 0–9 and their major outputs.
2. **Initial entity relationship** at Step 2, then update it at Step 6 to include the optional category relationship without duplicating the full diagram.
3. **Read versus update flow** at Step 3: `Include`/`AsNoTracking` versus `Find`/tracked entity/`SaveChanges`.
4. **Add Contact sequence** at Step 4: prompt -> validation -> service -> context -> SQLite.
5. **Migration timeline** at Step 9: InitialCreate -> AddCategories -> AddUniqueCategoryName.

## Accuracy requirements

- Describe search as SQL `LIKE` substring matching.
- Explain that `DbSet.Add` marks a new entity for insertion and `SaveChanges` converts tracked additions, modifications, and deletions into database commands.
- Explain that `OnConfiguring` selects SQLite and that a context is the unit through which EF Core knows the model, connection, and tracked changes.
- Explain `Include` and `AsNoTracking` for reads; explain `Find` and copied values for updates.
- State that category service code supplies the case-insensitive duplicate policy while the database has a unique name index.
- State that the Designer files and model snapshot are generated EF Core metadata and should not be hand-edited.
- State that configuration is local and ignored because it can contain plaintext secrets.
- State that InMemory tests do not prove SQLite query translation, index, or migration behavior.

## Verification

- Confirm the tutorial has Step 0 through Step 9 in order and each contains all seven teaching fields.
- Confirm every source/test/migration/project/asset file is named once in its introduction step or final reference.
- Confirm each first-use C# and EF Core term has a plain-language definition.
- Confirm prose headings and explanations are Vietnamese while source identifiers and commands remain unchanged.
- Confirm exactly five Mermaid fences, valid repository-relative links, readable tables, and copyable commands.
- Search for `file://`, `TODO`, `TBD`, `Latest`, stale runtime-first heading names, and claims of historical creation order.
- Run `dotnet test PhoneBook.Tests/PhoneBook.Tests.csproj` without source-code changes.
