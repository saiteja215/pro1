# EcommerceBackend

This project provides the backend API for an e-commerce application built with ASP.NET Core.

## Using Swagger

When running in development mode, Swagger UI is available for testing the API. Some endpoints require admin authorization. To access these endpoints, click the **Authorize** button in Swagger UI and provide a JWT token using the following format:

```
Authorization: Bearer <valid-admin-token>
```

Replace `<valid-admin-token>` with a valid JWT for a user in the `Admin` role.

### Admin token endpoint

You can request a token for an admin user by calling the `/api/auth/admin-token`
endpoint with admin credentials. The endpoint returns an `AuthResponseDto` object containing the JWT token and the logged in user's information:

```
POST /api/auth/admin-token
{
  "email": "admin@example.com",
  "password": "AdminPass123"
}
```

Example response:

```
{
  "token": "<jwt-token>",
  "user": {
    "id": 1,
    "fullName": "Admin User",
    "email": "admin@example.com",
    "role": "Admin"
  }
}
```

Use the returned token in the `Authorization` header when adding products.
