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
order by Freight desc