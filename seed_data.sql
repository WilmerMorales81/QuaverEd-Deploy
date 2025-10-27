-- Script to insert test data in QuaverEd
-- Execute in PostgreSQL

-- Clean existing data
DELETE FROM order_items;
DELETE FROM orders;
DELETE FROM instruments;
DELETE FROM customers;

-- Insert customers
INSERT INTO customers ("Name", "Address", "Phone", "Email", "ContactDate", "CreatedAt", "UpdatedAt") VALUES
('John Smith', '123 Main Street, New York', '555-0101', 'john.smith@email.com', '2024-01-15', NOW(), NOW()),
('Sarah Johnson', '456 Central Avenue, Los Angeles', '555-0102', 'sarah.johnson@email.com', '2024-02-20', NOW(), NOW()),
('Michael Brown', '789 Oak Street, Chicago', '555-0103', 'michael.brown@email.com', '2024-03-10', NOW(), NOW()),
('Emily Davis', '321 Pine Road, Houston', '555-0104', 'emily.davis@email.com', '2024-04-05', NOW(), NOW()),
('David Wilson', '654 Elm Boulevard, Phoenix', '555-0105', 'david.wilson@email.com', '2024-05-12', NOW(), NOW());

-- Insert instruments
INSERT INTO instruments ("ModelNumber", "Name", "Manufacturer", "Category", "RetailPrice", "WholesalePrice", "QuantityOnHand", "CreatedAt", "UpdatedAt") VALUES
('FENDER-001', 'Stratocaster Standard', 'Fender', 'guitar', 899.99, 450.00, 15, NOW(), NOW()),
('GIBSON-002', 'Les Paul Studio', 'Gibson', 'guitar', 1299.99, 650.00, 8, NOW(), NOW()),
('YAMAHA-003', 'YAS-280 Alto Saxophone', 'Yamaha', 'brass', 1299.99, 650.00, 12, NOW(), NOW()),
('ROLAND-004', 'TD-17KV Electronic Drum Kit', 'Roland', 'percussion', 799.99, 400.00, 6, NOW(), NOW()),
('STEINWAY-005', 'Model M Grand Piano', 'Steinway', 'piano', 89999.99, 45000.00, 2, NOW(), NOW()),
('MARTIN-006', 'D-28 Acoustic Guitar', 'Martin', 'guitar', 3299.99, 1650.00, 4, NOW(), NOW()),
('TRUMPET-007', 'Bach TR300 Trumpet', 'Bach', 'brass', 899.99, 450.00, 10, NOW(), NOW()),
('PEARL-008', 'Vision Birch Drum Kit', 'Pearl', 'percussion', 1299.99, 650.00, 7, NOW(), NOW());

-- Insert orders
INSERT INTO orders ("CustomerId", "OrderDate", "ShipDate", "Notes", "Status", "TotalAmount", "CreatedAt", "UpdatedAt") VALUES
(1, '2024-01-20', '2024-01-22', 'Urgent delivery', 'shipped', 899.99, NOW(), NOW()),
(2, '2024-02-25', '2024-02-27', 'VIP customer', 'shipped', 1299.99, NOW(), NOW()),
(3, '2024-03-15', '2024-03-17', 'First purchase', 'shipped', 799.99, NOW(), NOW()),
(1, '2024-04-10', '2024-04-12', 'Second purchase', 'shipped', 3299.99, NOW(), NOW()),
(4, '2024-05-15', NULL, 'Pending shipment', 'paid', 899.99, NOW(), NOW()),
(5, '2024-06-01', NULL, 'Recent order', 'pending', 1299.99, NOW(), NOW()),
(2, '2024-06-05', NULL, 'Frequent customer', 'pending', 89999.99, NOW(), NOW());

-- Insert order items
INSERT INTO order_items ("OrderId", "InstrumentId", "Quantity", "UnitPrice", "TotalPrice", "CreatedAt") VALUES
-- Order 1: John bought Stratocaster
(1, 1, 1, 899.99, 899.99, NOW()),

-- Order 2: Sarah bought Les Paul
(2, 2, 1, 1299.99, 1299.99, NOW()),

-- Order 3: Michael bought Drum Kit
(3, 4, 1, 799.99, 799.99, NOW()),

-- Order 4: John bought Martin D-28
(4, 6, 1, 3299.99, 3299.99, NOW()),

-- Order 5: Emily bought Trumpet
(5, 7, 1, 899.99, 899.99, NOW()),

-- Order 6: David bought Pearl Drum Kit
(6, 8, 1, 1299.99, 1299.99, NOW()),

-- Order 7: Sarah bought Steinway Piano
(7, 5, 1, 89999.99, 89999.99, NOW());

-- Verify inserted data
SELECT 'Customers:' as table_name, COUNT(*) as count FROM customers
UNION ALL
SELECT 'Instruments:', COUNT(*) FROM instruments
UNION ALL
SELECT 'Orders:', COUNT(*) FROM orders
UNION ALL
SELECT 'Order Items:', COUNT(*) FROM order_items;
