-- Insertar datos de prueba
INSERT INTO customers ("Name", "Email", "CreatedAt", "UpdatedAt") 
VALUES 
('Juan Pérez', 'juan.perez@email.com', NOW(), NOW()),
('María García', 'maria.garcia@email.com', NOW(), NOW()),
('Carlos López', 'carlos.lopez@email.com', NOW(), NOW());

INSERT INTO instruments ("ModelNumber", "Name", "Manufacturer", "Category", "RetailPrice", "WholesalePrice", "QuantityOnHand", "CreatedAt", "UpdatedAt")
VALUES 
('FENDER-001', 'Stratocaster Standard', 'Fender', 'guitar', 899.99, 450.00, 15, NOW(), NOW()),
('GIBSON-002', 'Les Paul Studio', 'Gibson', 'guitar', 1299.99, 650.00, 8, NOW(), NOW()),
('YAMAHA-003', 'YAS-280 Alto Saxophone', 'Yamaha', 'brass', 1299.99, 650.00, 12, NOW(), NOW());

INSERT INTO orders ("CustomerId", "OrderDate", "ShipDate", "Status", "TotalAmount", "CreatedAt", "UpdatedAt")
VALUES 
(1, '2024-01-20', '2024-01-22', 'shipped', 899.99, NOW(), NOW()),
(2, '2024-02-25', '2024-02-27', 'shipped', 1299.99, NOW(), NOW()),
(3, '2024-03-15', '2024-03-17', 'shipped', 799.99, NOW(), NOW());

INSERT INTO order_items ("OrderId", "InstrumentId", "Quantity", "UnitPrice", "TotalPrice", "CreatedAt")
VALUES 
(1, 1, 1, 899.99, 899.99, NOW()),
(2, 2, 1, 1299.99, 1299.99, NOW()),
(3, 3, 1, 799.99, 799.99, NOW());
