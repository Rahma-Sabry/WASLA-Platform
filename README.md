# Hiring Platform - Connecting Employers and Job Seekers

## Overview

This platform revolutionizes hiring by directly connecting employers and job seekers through an intuitive and efficient system. Utilizing a virtual coin-based economy, the platform streamlines job posting transactions and fosters a secure and transparent environment for recruitment.

This document outlines the core functionalities and workflow of the Hiring Platform.

---

## ✅ Implemented Features

### 🔹 User Roles
- **Employers** can post job openings and manage applications.
- **Employees (Job Seekers)** can create professional profiles and apply for jobs.

### 🔹 Seamless Onboarding
- **Employer Registration** with company details and initial coin allocation.
- **Employee Registration** with personal details and resume upload.

### 🔹 Job Posting
- Intuitive job creation form (title, description, type, salary, category).
- Jobs are saved as drafts initially.
- Automatic coin cost calculation for publishing jobs.

### 🔹 Application Management
- Employers can:
  - Review applications via a dashboard.
  - Schedule interviews.
  - Reject or shortlist candidates.

### 🔹 Security and Verification
- Email and phone verification for all users.
- Optional document uploads (e.g., resumes, IDs).

---

## 🚧 Features in Progress

### 🔹 Hiring Workflow
- Mark applications as **“Accepted”**.
- Mark job posts as **“Filled”**.
- Trigger coin deduction upon hiring.

### 🔹 Coin-Based Economy
- Track coin balances via employer wallets.
- Allow employers to purchase coins.
- Log all coin transactions.

### 🔹 User Feedback System
- Basic rating and comment mechanism.
- Store feedback to build user reputation.
- Feedback moderation system.

### 🔹 Additional Planned Features
- Antivirus scanning for uploaded documents.
- Admin tools for reviewing suspicious behavior.
- In-app or email notification system.
- Employee-job recommendation engine.
- Employer analytics dashboard (e.g., views, engagement).

---

## Backend Data Model

### 🔹 Core Tables
- **User** – Base table for user accounts.
- **Recruiter** – Recruiter-specific info.
- **Employee** – Job seeker profile.
- **Job** – Job listings.
- **Application** – Tracks job applications.
- **CoinHistory** – Virtual coin economy.
- **Feedback** – Post-hire ratings and reviews.

### 🔹 Supporting Tables
- **ApplicationHistory**
- **ApplicationStatus**
- **Education**
- **Experience**
- **Skill** / **EmployeeSkill**
- **JobType**, **ProcessType**, **Degree**
- **WaslaContext**, **WaslaSecurityContext**
- **ErrorViewModel**, **DTO** (not stored in DB)

---

## Getting Started (Example - Developer Setup)

1. **Clone the repository:**
    ```bash
    git clone [repository URL]
    cd hiring-platform
    ```

2. **Set up the backend environment:**
    - Install required dependencies.
    - Configure the database.
    - Run migrations to create tables.

3. **Set up the frontend environment:** (if applicable)
    - Install dependencies.
    - Connect to the backend API.
    - Build the app.

4. **Run the application:**
    - Start backend and frontend servers.

5. **Access the platform:**
    Open your browser and visit the platform’s local or hosted URL.

---

## Contributing

We welcome contributions! Please follow these steps:

1. Fork the repository.
2. Create a new branch for your changes.
3. Commit your changes with clear messages.
4. Push to your fork.
5. Submit a pull request with a detailed description.
