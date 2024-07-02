select
ProductID,
ProductName,
SupplierID,
CategoryID,
QuantityPerUnit,
UnitPrice,
UnitsInStock,
ReorderLevel,
Discontinued

from
Products

where
UnitsInStock >= 40
