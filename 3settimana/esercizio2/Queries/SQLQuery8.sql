select
OrderID,
CustomerID,
EmployeeID,
OrderDate,
RequiredDate,
ShippedDate,
ShipVia,
Freight,
ShipName,
ShipAddress,
ShipCity,
ShipRegion,
ShipPostalCode,
ShipCountry

from
Orders

where
Freight > 90 and Freight < 200