# Radio Call List Manager

[![Buy Me a Coffee](https://img.shields.io/badge/Buy%20Me%20a%20Coffee-Support%20my%20projects-yellow?logo=buymeacoffee)](https://www.buymeacoffee.com/jackliu56)

![Application Screenshot](docs/Screenshot.jpg)

Radio Call List Manager is a Windows Forms desktop application built with VB.NET.  
It is designed to help radio system administrators import, review, validate, edit, and export radio contact records for different vendor-specific programming formats.

This project was built as a practical portfolio application to demonstrate VB.NET WinForms development, data validation, file processing, user interface design, and export workflow automation.

---

## Features

### 1. Import Radio Contact Files

The application supports importing radio contact records from:

- CSV files
- XML files

Imported records are loaded into a DataGridView for review and editing.

Supported contact fields include:

- Radio ID
- Callsign
- IP Address
- MDT IP Address
- Radio User
- Group ID

---

### 2. Record Editing

Users can select a record and view its details in the record detail panel.

The application supports:

- Editing existing records
- Adding new records
- Applying changes
- Resetting unsaved changes
- Deleting selected records

Changes are tracked so the user can be warned before losing unsaved work.

---

### 3. Validation

The validation tool checks imported records for common data issues before export.

Validation checks include:

- Missing Radio ID
- Non-numeric Radio ID
- Duplicate Radio ID
- Missing alias source
- Radio ID length issues
- Alias length issues

The validation summary panel displays:

- Total records
- Valid records
- Error records
- Duplicate records
- Missing field issues
- Length violations

Invalid records are automatically selected to help the user locate issues quickly.

---

### 4. Find and Duplicate Detection

The application includes search and duplicate detection tools.

Supported tools:

- Find records by Radio ID, callsign, IP address, radio user, or group ID
- Find duplicate Radio IDs
- Automatically select all duplicate records

---

### 5. Vendor-Specific Export

The application can export call list files in multiple vendor-specific formats.

Supported export formats include:

- Motorola APX
- Motorola XTS
- EF Johnson (In Developing)
- Harris RPM (In Developing)

The export process generates aliases based on user-defined settings:

- Alias Length
- ID Suffix Length

Example:

```text
Radio User: Page Bot
Radio ID: 9301
Alias Length: 4
ID Length: 2

Export Alias: Page 01