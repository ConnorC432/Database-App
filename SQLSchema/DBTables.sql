CREATE TABLE customer_table (
Customer_ID INT,
Customer_FirstName VARCHAR(50) NOT NULL,
Customer_LastName VARCHAR(50) NOT NULL,
Customer_DOB DATE NOT NULL,
Customer_Email VARCHAR(50) NOT NULL,
CONSTRAINT pk_customer PRIMARY KEY (Customer_ID)
);

CREATE TABLE item_table (
Item_ID INT,
Item_Name VARCHAR(50) NOT NULL,
Item_Desc VARCHAR(100) NOT NULL,
Item_Price DOUBLE NOT NULL,
CONSTRAINT pk_item PRIMARY KEY (Item_ID)
);

CREATE TABLE order_table (
Order_ID INT,
Order_Time DATETIME NOT NULL,
Order_Customer_ID INT NOT NULL,
Order_Address VARCHAR(50) NOT NULL,
CONSTRAINT pk_order PRIMARY KEY (Order_ID),
CONSTRAINT fk_customer FOREIGN KEY (Order_Customer_ID) REFERENCES customer_table(Customer_ID) ON UPDATE CASCADE ON DELETE NO ACTION
);

CREATE TABLE order_items (
Order_ID INT,
Item_ID INT,
Quantity INT NOT NULL,
CONSTRAINT pk_orderitem PRIMARY KEY (Order_ID,Item_ID),
CONSTRAINT fk_orderitem FOREIGN KEY (Order_ID) REFERENCES order_table(Order_ID) ON UPDATE CASCADE ON DELETE NO ACTION,
CONSTRAINT fk_itemid FOREIGN KEY (Item_ID) REFERENCES item_table(Item_ID) ON UPDATE CASCADE ON DELETE NO ACTION
);