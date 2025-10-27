# QuaverEd API Documentation

## Overview

The QuaverEd API provides comprehensive endpoints for managing customers, instruments, orders, and generating business reports for an online musical instrument store.

**Base URL**: `http://localhost:5163/api`

## Authentication

Currently, the API does not require authentication. All endpoints are publicly accessible.

## Response Format

All API responses follow a consistent JSON format:

```json
{
  "message": "Description of the operation",
  "count": 5,
  "data": [...]
}
```

## Error Handling

The API returns appropriate HTTP status codes:

- `200 OK`: Successful request
- `400 Bad Request`: Invalid parameters
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

Error responses include details:

```json
{
  "message": "Error description",
  "error": "Detailed error message"
}
```

---

## Customer Endpoints

### Search Customers

Search customers by personal data with substring support.

**Endpoint**: `GET /customers/search`

**Parameters**:
- `name` (optional): Customer name substring
- `email` (optional): Email substring
- `address` (optional): Address substring

**Example Request**:
```http
GET /api/customers/search?name=John
GET /api/customers/search?email=smith
GET /api/customers/search?address=New York
```

**Example Response**:
```json
{
  "message": "Customer search completed",
  "count": 1,
  "customers": [
    {
      "id": 4,
      "name": "John Smith",
      "email": "john.smith@email.com",
      "phone": "555-0101",
      "address": "123 Main Street, New York",
      "contactDate": "2024-01-15T00:00:00Z"
    }
  ]
}
```

### Get Customer Summary

Get all customers with their order totals.

**Endpoint**: `GET /customers/summary`

**Example Request**:
```http
GET /api/customers/summary
```

**Example Response**:
```json
{
  "message": "Customer summary retrieved successfully",
  "count": 5,
  "customers": [
    {
      "id": 4,
      "name": "John Smith",
      "email": "john.smith@email.com",
      "totalOrders": 2,
      "totalAmount": 4199.98
    },
    {
      "id": 5,
      "name": "Sarah Johnson",
      "email": "sarah.johnson@email.com",
      "totalOrders": 2,
      "totalAmount": 91299.98
    }
  ]
}
```

### Get Customer Instruments

Get all instruments purchased by a specific customer.

**Endpoint**: `GET /customers/{id}/instruments`

**Parameters**:
- `id` (path): Customer ID

**Example Request**:
```http
GET /api/customers/4/instruments
```

**Example Response**:
```json
{
  "message": "Instruments purchased by John Smith",
  "customer": {
    "id": 4,
    "name": "John Smith",
    "email": "john.smith@email.com"
  },
  "instrumentsCount": 2,
  "instruments": [
    {
      "id": 4,
      "name": "Stratocaster Standard",
      "modelNumber": "FENDER-001",
      "manufacturer": "Fender",
      "category": "guitar",
      "quantityPurchased": 1,
      "purchasePrice": 899.99,
      "orderDate": "2024-01-20T00:00:00Z"
    },
    {
      "id": 9,
      "name": "D-28 Acoustic Guitar",
      "modelNumber": "MARTIN-006",
      "manufacturer": "Martin",
      "category": "guitar",
      "quantityPurchased": 1,
      "purchasePrice": 3299.99,
      "orderDate": "2024-04-10T00:00:00Z"
    }
  ]
}
```

---

## Order Endpoints

### Get Orders by Date Range

Retrieve orders within a specific date range.

**Endpoint**: `GET /orders/date-range`

**Parameters**:
- `from` (required): Start date (YYYY-MM-DD)
- `to` (required): End date (YYYY-MM-DD)

**Example Request**:
```http
GET /api/orders/date-range?from=2024-01-01&to=2024-03-31
```

**Example Response**:
```json
{
  "message": "Orders found between 2024-01-01 and 2024-03-31",
  "dateRange": {
    "from": "2024-01-01",
    "to": "2024-03-31"
  },
  "count": 3,
  "orders": [
    {
      "id": 11,
      "orderDate": "2024-01-20T00:00:00Z",
      "shipDate": "2024-01-22T00:00:00Z",
      "status": "shipped",
      "totalAmount": 899.99,
      "notes": "Urgent delivery",
      "customer": {
        "id": 4,
        "name": "John Smith",
        "email": "john.smith@email.com"
      },
      "itemsCount": 1,
      "items": [
        {
          "name": "Stratocaster Standard",
          "modelNumber": "FENDER-001",
          "quantity": 1,
          "unitPrice": 899.99,
          "totalPrice": 899.99
        }
      ]
    }
  ]
}
```

### Get Orders by Price Range

Retrieve orders within a specific price range.

**Endpoint**: `GET /orders/price-range`

**Parameters**:
- `min` (required): Minimum order amount
- `max` (required): Maximum order amount

**Example Request**:
```http
GET /api/orders/price-range?min=1000&max=5000
```

**Example Response**:
```json
{
  "message": "Orders found between $1000.00 and $5000.00",
  "priceRange": {
    "min": 1000.00,
    "max": 5000.00
  },
  "count": 2,
  "orders": [
    {
      "id": 12,
      "orderDate": "2024-02-25T00:00:00Z",
      "shipDate": "2024-02-27T00:00:00Z",
      "status": "shipped",
      "totalAmount": 1299.99,
      "notes": "VIP customer",
      "customer": {
        "id": 5,
        "name": "Sarah Johnson",
        "email": "sarah.johnson@email.com"
      },
      "itemsCount": 1,
      "items": [
        {
          "name": "Les Paul Studio",
          "modelNumber": "GIBSON-002",
          "quantity": 1,
          "unitPrice": 1299.99,
          "totalPrice": 1299.99
        }
      ]
    }
  ]
}
```

---

## Instrument Endpoints

### Get Sold Instruments

Get instruments sold within a specific time period with sales statistics.

**Endpoint**: `GET /instruments/sold`

**Parameters**:
- `from` (required): Start date (YYYY-MM-DD)
- `to` (required): End date (YYYY-MM-DD)

**Example Request**:
```http
GET /api/instruments/sold?from=2024-01-01&to=2024-12-31
```

**Example Response**:
```json
{
  "message": "Instruments sold between 2024-01-01 and 2024-12-31",
  "period": {
    "from": "2024-01-01",
    "to": "2024-12-31"
  },
  "instrumentsCount": 7,
  "totalRevenue": 95199.92,
  "instruments": [
    {
      "instrument": {
        "id": 8,
        "name": "Model M Grand Piano",
        "modelNumber": "STEINWAY-005",
        "manufacturer": "Steinway",
        "category": "piano"
      },
      "totalQuantitySold": 1,
      "totalRevenue": 89999.99,
      "averagePrice": 89999.99,
      "ordersCount": 1
    },
    {
      "instrument": {
        "id": 9,
        "name": "D-28 Acoustic Guitar",
        "modelNumber": "MARTIN-006",
        "manufacturer": "Martin",
        "category": "guitar"
      },
      "totalQuantitySold": 1,
      "totalRevenue": 3299.99,
      "averagePrice": 3299.99,
      "ordersCount": 1
    }
  ]
}
```

---

## Report Endpoints

### Get Customers by Instrument Type

Get all customers who have ordered instruments of a specific category.

**Endpoint**: `GET /reports/customers-by-instrument-type`

**Parameters**:
- `category` (required): Instrument category (e.g., "guitar", "brass", "percussion", "piano")

**Example Request**:
```http
GET /api/reports/customers-by-instrument-type?category=guitar
```

**Example Response**:
```json
{
  "message": "Customers who have ordered guitar instruments",
  "category": "guitar",
  "customersCount": 2,
  "customers": [
    {
      "id": 4,
      "name": "John Smith",
      "email": "john.smith@email.com",
      "phone": "555-0101",
      "address": "123 Main Street, New York",
      "totalOrdersInCategory": 2,
      "totalSpentInCategory": 4199.98,
      "instruments": [
        {
          "name": "Stratocaster Standard",
          "modelNumber": "FENDER-001",
          "quantity": 1,
          "unitPrice": 899.99,
          "totalPrice": 899.99,
          "orderDate": "2024-01-20T00:00:00Z"
        },
        {
          "name": "D-28 Acoustic Guitar",
          "modelNumber": "MARTIN-006",
          "quantity": 1,
          "unitPrice": 3299.99,
          "totalPrice": 3299.99,
          "orderDate": "2024-04-10T00:00:00Z"
        }
      ]
    }
  ]
}
```

---

## Data Models

### Customer

```json
{
  "id": 4,
  "name": "John Smith",
  "address": "123 Main Street, New York",
  "phone": "555-0101",
  "email": "john.smith@email.com",
  "contactDate": "2024-01-15T00:00:00Z",
  "createdAt": "2024-01-15T10:30:00Z",
  "updatedAt": "2024-01-15T10:30:00Z"
}
```

### Instrument

```json
{
  "id": 4,
  "modelNumber": "FENDER-001",
  "name": "Stratocaster Standard",
  "manufacturer": "Fender",
  "category": "guitar",
  "retailPrice": 899.99,
  "wholesalePrice": 450.00,
  "quantityOnHand": 15,
  "createdAt": "2024-01-15T10:30:00Z",
  "updatedAt": "2024-01-15T10:30:00Z"
}
```

### Order

```json
{
  "id": 11,
  "customerId": 4,
  "orderDate": "2024-01-20T00:00:00Z",
  "shipDate": "2024-01-22T00:00:00Z",
  "notes": "Urgent delivery",
  "status": "shipped",
  "totalAmount": 899.99,
  "createdAt": "2024-01-20T10:30:00Z",
  "updatedAt": "2024-01-20T10:30:00Z"
}
```

### OrderItem

```json
{
  "id": 1,
  "orderId": 11,
  "instrumentId": 4,
  "quantity": 1,
  "unitPrice": 899.99,
  "totalPrice": 899.99,
  "createdAt": "2024-01-20T10:30:00Z"
}
```

---

## Status Codes

### Order Status
- `pending`: Order placed but not yet paid
- `paid`: Order paid but not yet shipped
- `shipped`: Order shipped to customer
- `delivered`: Order delivered to customer
- `cancelled`: Order cancelled

### Instrument Categories
- `guitar`: Electric and acoustic guitars
- `brass`: Trumpets, saxophones, etc.
- `percussion`: Drums, drum kits, etc.
- `piano`: Grand pianos, upright pianos, etc.

---

## Rate Limiting

Currently, there are no rate limits implemented. For production deployment, consider implementing rate limiting based on your requirements.

## CORS

CORS is configured to allow requests from any origin in development. For production, configure appropriate CORS policies.

---

## Support

For technical support or questions about the API, please refer to the main README.md file or open an issue in the repository.