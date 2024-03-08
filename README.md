# Enhanced Banking API with .NET 7 (Fimple .Net Final Case)

## Entrance
Today, banking services are becoming increasingly digital and customer expectations are constantly evolving. This change requires banking systems to be equipped with secure, effective and user-friendly web services. This project aims to develop RESTful APIs on the .NET platform.

## Features
- User registration and management.
- Account transactions, balance inquiry and update.
- Money transfers and transaction records.
- Loan application, payment plans and inquiries.
- Automatic payment settings and management.
- Customer support requests and follow-up.

## Technologies
- .Net Core
- SQL Server
- Authentication with JWT
- Entity Framework

## API Details and Documentation

## (Users Controller)
- Post  /api/users/register : Creates a new user (Admin only).
- Get   /api/users/get-all-users : Displays all users (Admin only).
- Put   /api/users/role-assign : Makes role assignment (Admin only).

## (Accounts Controller)
- Post  /api/accounts/create-account : Creates a new account (Admin only).
- Get   /api/accounts/{accountId}/balance : Queries the balance of a specific account.
- Put   /api(accounts/id/update-balance : Updates the balance of a specific account (Admin only).

## (Login Controller)
- Post   /api/Login/login : User logs in and returns JWT token.

## (Transactions Controller)
- Post   /api/transactions/withdraw : Withdrawing money from the account.
- Post   /api/transactions/deposit : Deposit money into the account.
- Post   /api/transactions/transfer/internal : Internal transfer between accounts.
- Post   /api/transactions/transfer/external : External transfer between accounts.
