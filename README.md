# Submission Management System

This project is a comprehensive Windows Forms Application for managing user submissions. It includes functionalities for viewing, editing, deleting, and searching user submissions, with a user-friendly interface.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Forms](#forms)
- [API Endpoints](#api-endpoints)
- [Error Handling](#ErrorHandling)
- [Dependencies](#Dependencies)
- [Contact](#contact)

## Overview

The Submission Management System allows users to view, edit, delete, and search through user submissions efficiently. It interacts with a backend server to retrieve and manipulate submission data.

## Features

- **Create Submissions**: Create a submission. Added a stopwatch to keep track of time taken for submission. User can also pause the time and resume. 
- **View Submissions**: User can view details like Name, Email, Phone Number, Github Link and time taken for submission.
- **Edit Submissions**: Modify details of a submission.
- **Delete Submissions**: Remove a specific submission.
- **Search Submissions**: Look up a submission by email ID.
- **User-Friendly Interface**: Easy-to-use forms and controls. Added shortcuts for viewing, creating, previous, next , toggle stopwatch button.

## Installation

### Prerequisites

- .NET Framework 4.7.2 or higher
- Visual Studio 2019 or higher
- Node.js and npm

### Steps

1. Clone the repository:
   ```sh
   git clone https://github.com/TJ2206/WindowsFormsApp_SA.git


2. Clone the repository:
   ```sh
   cd WindowsFormsApp_SA/backend
   npm install

3. Start the backend server:
   ```sh
   npm start

4. Open the Project in Visual Studio:
   ```sh
   SlidelyAI_final.sln

5. Run the application:

Press F5 or click Start in Visual Studio to run the application.

## Usage

### Viewing Submissions

Use the "Previous" and "Next" buttons on the ViewSubmissionsForm to navigate through the submissions.

### Deleting Submissions

Click the "Delete Entry" button to delete the current submission.

### Searching by Email

1. Enter the email ID in the "Search by Email" text box.
2. Click the "Search by Email" button to find the submission.

### Creating Submissions

1. Open the CreateSubmissionsForm from the main menu.
2. Enter the details for the new submission.
3. Click "Submit" to add the new submission to the database.

## Forms

### MainForm

The main entry point of the application, providing navigation to various functionalities such as creating and managing submissions.

### CreateSubmissionForm

Enables users to create new submissions by entering Name, Email, Phone Number, GithubLink.


### ViewSubmissionsForm

Allows users to view and navigate through submissions. Includes buttons for deleting and searching submissions.

## API Endpoints

1. **Health Check**

   **GET /ping**
   
   - **Description:** Health check endpoint to verify if the server is running.
   - **Response:** `true` if server is running.

2. **Submitting a New Submission**

   **POST /submit**
   
   - **Description:** Create a new submission entry.
   - **Request Body:**
     ```json
     {
       "name": "John Doe",
       "email": "john.doe@example.com",
       "phone": "123-456-7890",
       "github_link": "https://github.com/johndoe",
       "stopwatch_time": "00:15:30"
     }
     ```
   - **Response:** "Submission saved successfully" on success, or error message on failure.

3. **Retrieving All Submissions**

   **GET /submissions**
   
   - **Description:** Retrieve all submissions.
   - **Response:** JSON array of submission objects.

4. **Retrieving a Specific Submission**

   **GET /read?index=<index>**
   
   - **Description:** Retrieve a submission by its index.
   - **Parameters:**
     - `index`: Index of the submission to retrieve.
   - **Response:** JSON object of the submission at the specified index, or "Entry not found" if not found.

5. **Deleting a Submission**

   **DELETE /delete?index=<index>**
   
   - **Description:** Delete a submission by its index.
   - **Parameters:**
     - `index`: Index of the submission to delete.
   - **Response:** "Submission deleted successfully" on success, or "Entry not found" if submission does not exist.

6. **Updating a Submission**

   **PUT /edit**
   
   - **Description:** Update a submission by its index.
   - **Request Body:**
     ```json
     {
       "index": 0,
       "name": "Updated Name",
       "email": "updated.email@example.com",
       "phone": "987-654-3210",
       "github_link": "https://github.com/updateduser",
       "stopwatch_time": "00:20:00"
     }
     ```
   - **Response:** "Submission updated successfully" on success, or "Entry not found" if submission does not exist.

7. **Searching for a Submission by Email**

   **GET /search?email=<email>**
   
   - **Description:** Search for a submission by email.
   - **Parameters:**
     - `email`: Email address to search for.
   - **Response:** JSON object of the submission with the specified email, or "Submission not found" if not found.

### Error Handling

- HTTP status codes `500` for server errors and `404` for resource not found are used appropriately for error responses.

### Dependencies

- `express`: Web framework for Node.js
- `body-parser`: Middleware for parsing JSON request bodies
- `cors`: Middleware for handling Cross-Origin Resource Sharing
- `fs` and `path`: Node.js modules for file system operations and path manipulation

### Contact

-Name- Tanuj Chaudhary
, Email- tanujc2206@gmail.com
