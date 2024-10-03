USE MASTER

DROP DATABASE SWP391


CREATE DATABASE SWP391

USE SWP391


DROP TABLE [UtilityPod]
GO
DROP TABLE [SlotBooking]
GO
DROP TABLE [BookingOrder]
GO
DROP TABLE [Payment]
GO
DROP TABLE [Booking]
GO
DROP TABLE [Slot]
GO
DROP TABLE [Pod]
GO
DROP TABLE [Utility]
GO
DROP TABLE [Product]
GO
DROP TABLE [Store]
GO
DROP TABLE [User]
GO
DROP TABLE [Type]
GO
DROP TABLE [Category]
GO


CREATE TABLE [Utility] (
	Id				INT NOT NULL,
	Name			NVARCHAR(255) NULL,
	Image			NVARCHAR(255) NULL,
	Description		NVARCHAR(255) NULL,
	PRIMARY KEY (Id)
);

CREATE TABLE [Pod] (
	Id				INT NOT NULL,
	Name			NVARCHAR(255) NULL,
	Image			NVARCHAR(255) NULL,
	Description		NVARCHAR(255) NULL,
	Rating			INT NULL,
	Status			NVARCHAR(255) NULL,
	TypeId			INT NOT NULL,
	StoreId			INT NOT NULL,
	PRIMARY KEY (Id)
);

CREATE TABLE [UtilityPod] (
	UtilityId		INT NOT NULL,
	PodId			INT NOT NULL,
	PRIMARY KEY (UtilityId, PodId)
);

CREATE TABLE [Type] (
	Id				INT NOT NULL,
	Name			NVARCHAR(255) NULL,
	Capacity		INT NULL,
	PRIMARY KEY (Id)
);

CREATE TABLE [Slot] (
	Id				INT NOT NULL,
	Name			NVARCHAR(255) NULL,
	StartTime		INT NULL,
	EndTime			INT NULL,
	Price			INT NULL,
	Status			NVARCHAR(255) NULL,
	PodId			INT NOT NULL,
	PRIMARY KEY (Id)
);

CREATE TABLE [Booking] (
	Id				INT NOT NULL,
	Date			DATE NULL,
	Status			NVARCHAR(255) NULL,
	Feedback		NVARCHAR(255) NULL,
	PodId			INT NOT NULL,
	UserId			INT NOT NULL,
	PRIMARY KEY (Id)
);

CREATE TABLE [SlotBooking] (
	SlotId			INT NOT NULL,
	BookingId		INT NOT NULL,
	PRIMARY KEY (SlotId, BookingId)
);

CREATE TABLE [Payment] (
	Id				INT NOT NULL,
	Method			NVARCHAR(255) NULL,
	Amount			INT NULL,
	Date			DATE NULL,
	Status			NVARCHAR(255) NULL,
	BookingId		INT NOT NULL,
	PRIMARY KEY (Id)
);

CREATE TABLE [User] (
	Id				INT NOT NULL,
	Email			NVARCHAR(255) NULL,
	Password		NVARCHAR(255) NULL,
	Name			NVARCHAR(255) NULL,
	Image			NVARCHAR(255) NULL,
	Role			NVARCHAR(255) CHECK (Role IN ('Admin','Staff','User')),
	Type			NVARCHAR(255) CHECK (Type IN ('VIP','Regular')),
	PhoneNumber		NVARCHAR(255) NULL,
	Point			INT NULL,
	Description		NVARCHAR(255) NULL,
	PRIMARY KEY (Id)
);

CREATE TABLE [Store] (
	Id				INT NOT NULL,
	Name			NVARCHAR(255) NULL,
	Address			NVARCHAR(255) NULL,
	Contact			NVARCHAR(255) NULL,
	PRIMARY KEY (Id)
);

CREATE TABLE [Product] (
	Id				INT NOT NULL,
	Name			NVARCHAR(255) NULL,
	Price			INT NULL,
	Description		NVARCHAR(255) NULL,
	Rating			INT NULL,
	Stock			INT NULL,
	StoreId			INT NOT NULL,
	CategoryId		INT NOT NULL,
	PRIMARY KEY (Id)
);

CREATE TABLE [BookingOrder] (
	Id				INT NOT NULL,
	Amount			INT NULL,
	Quantity		INT NULL,
	Status			NVARCHAR(255) NULL,
	Date			DATE NULL,
	BookingId		INT NOT NULL,
	ProductId		INT NOT NULL,
	PRIMARY KEY (Id)
);

CREATE TABLE [Category] (
	Id				INT NOT NULL,
	Name			NVARCHAR(255) NULL,
	Status			NVARCHAR(255) NULL,
	PRIMARY KEY (Id)
);

ALTER TABLE [Pod] ADD CONSTRAINT FkPodType FOREIGN KEY (TypeId) REFERENCES [Type](Id);
ALTER TABLE [Pod] ADD CONSTRAINT FkPodStore FOREIGN KEY (StoreId) REFERENCES [Store](Id);
ALTER TABLE [Slot] ADD CONSTRAINT FkSlotPod FOREIGN KEY (PodId) REFERENCES [Pod](Id);
ALTER TABLE [Booking] ADD CONSTRAINT FkBookingPod FOREIGN KEY (PodId) REFERENCES [Pod](Id);
ALTER TABLE [Booking] ADD CONSTRAINT FkBookingUser FOREIGN KEY (UserId) REFERENCES [User](Id);
ALTER TABLE [Payment] ADD CONSTRAINT FkPaymentBooking FOREIGN KEY (BookingId) REFERENCES [Booking](Id);
ALTER TABLE [Product] ADD CONSTRAINT FkProductStore FOREIGN KEY (StoreId) REFERENCES [Store](Id);
ALTER TABLE [Product] ADD CONSTRAINT FkProductCategory FOREIGN KEY (CategoryId) REFERENCES [Category](Id);
ALTER TABLE [UtilityPod] ADD CONSTRAINT FkUtility FOREIGN KEY (UtilityId) REFERENCES [Utility](Id);
ALTER TABLE [UtilityPod] ADD CONSTRAINT FkPod FOREIGN KEY (PodId) REFERENCES [Pod](Id);
ALTER TABLE [SlotBooking] ADD CONSTRAINT FkSlot FOREIGN KEY (SlotId) REFERENCES [Slot](Id);
ALTER TABLE [SlotBooking] ADD CONSTRAINT FkBooking FOREIGN KEY (BookingId) REFERENCES [Booking](Id);
ALTER TABLE [BookingOrder] ADD CONSTRAINT FkBookingOrderBooking FOREIGN KEY (BookingId) REFERENCES [Booking](Id);
ALTER TABLE [BookingOrder] ADD CONSTRAINT FkBookingOrderProduct FOREIGN KEY (ProductId) REFERENCES [Product](Id);



--INSERT DATA INTO TABLES

--Insert into Store 2 cái
INSERT INTO [Store] (Id, Name, Address, Contact) VALUES
(1, N'Cơ sở 1', N'12 Đường Sáng Tạo, Thủ Đức', N'0922678301'),
(2, N'Cơ sở 2', N'34 Đại lộ Kinh Doanh, Thủ Đức', N'0995678017');

--Insert into Type 4 cái
INSERT INTO [Type] (Id, Name, Capacity) VALUES
(1, N'POD Đơn', 1),
(2, N'POD Đôi', 2),
(3, N'POD Nhóm', 6),
(4, N'POD Phòng họp', 10);


--Insert into Pod 24 cái
INSERT INTO [Pod] (Id, Name, Image, Description, Rating, Status, TypeId, StoreId) VALUES

--Store1

--POD đơn 80 70 60
(1, N'Pod Cao cấp', 'premium_pod.jpg', N'Một không gian làm việc sang trọng với các tiện nghi cao cấp', 5, N'Còn trống', 1, 1),
(2, N'Pod Tiêu Chuẩn', 'standard_pod.jpg', N'Một không gian làm việc tiêu chuẩn với đầy đủ các tính năng cần thiết', 4, N'Còn trống', 1, 1),
(3, N'Pod Sáng Tạo', 'creative_pod.jpg', N'Không gian thiết kế độc đáo, truyền cảm hứng sáng tạo', 4, N'Còn trống', 1, 1),

--POD đôi 100 90 80
(4, N'Pod Cao cấp', 'premium_pod.jpg', N'Một không gian làm việc sang trọng với các tiện nghi cao cấp', 5, N'Còn trống', 2, 1),
(5, N'Pod Tiêu Chuẩn', 'standard_pod.jpg', N'Một không gian làm việc tiêu chuẩn với đầy đủ các tính năng cần thiết', 4, N'Còn trống', 2, 1),
(6, N'Pod Sáng Tạo', 'creative_pod.jpg', N'Không gian thiết kế độc đáo, truyền cảm hứng sáng tạo', 4, N'Còn trống', 2, 1),

--POD nhóm 120 110 100
(7, N'Pod Cao cấp', 'premium_pod.jpg', N'Một không gian làm việc sang trọng với các tiện nghi cao cấp', 5, N'Còn trống', 3, 1),
(8, N'Pod Tiêu Chuẩn', 'standard_pod.jpg', N'Một không gian làm việc tiêu chuẩn với đầy đủ các tính năng cần thiết', 4, N'Còn trống', 3, 1),
(9, N'Pod Sáng Tạo', 'creative_pod.jpg', N'Không gian thiết kế độc đáo, truyền cảm hứng sáng tạo', 4, N'Còn trống', 3, 1),

--POD phòng họp 200 190 180
(10, N'Pod Cao cấp', 'premium_pod.jpg', N'Một không gian làm việc sang trọng với các tiện nghi cao cấp', 5, N'Còn trống', 4, 1),
(11, N'Pod Tiêu Chuẩn', 'standard_pod.jpg', N'Một không gian làm việc tiêu chuẩn với đầy đủ các tính năng cần thiết', 4, N'Còn trống', 4, 1),
(12, N'Pod Sáng Tạo', 'creative_pod.jpg', N'Không gian thiết kế độc đáo, truyền cảm hứng sáng tạo', 4, N'Còn trống', 4, 1),

--Store2

--POD đơn
(13, N'Pod Cao cấp', 'premium_pod.jpg', N'Một không gian làm việc sang trọng với các tiện nghi cao cấp', 5, N'Còn trống', 1, 2),
(14, N'Pod Tiêu Chuẩn', 'standard_pod.jpg', N'Một không gian làm việc tiêu chuẩn với đầy đủ các tính năng cần thiết', 4, N'Còn trống', 1, 2),
(15, N'Pod Sáng Tạo', 'creative_pod.jpg', N'Không gian thiết kế độc đáo, truyền cảm hứng sáng tạo', 4, N'Còn trống', 1, 2),

--POD đôi
(16, N'Pod Cao cấp', 'premium_pod.jpg', N'Một không gian làm việc sang trọng với các tiện nghi cao cấp', 5, N'Còn trống', 2, 2),
(17, N'Pod Tiêu Chuẩn', 'standard_pod.jpg', N'Một không gian làm việc tiêu chuẩn với đầy đủ các tính năng cần thiết', 4, N'Còn trống', 2, 2),
(18, N'Pod Sáng Tạo', 'creative_pod.jpg', N'Không gian thiết kế độc đáo, truyền cảm hứng sáng tạo', 4, N'Còn trống', 2, 2),

--POD nhóm
(19, N'Pod Cao cấp', 'premium_pod.jpg', N'Một không gian làm việc sang trọng với các tiện nghi cao cấp', 5, N'Còn trống', 3, 2),
(20, N'Pod Tiêu Chuẩn', 'standard_pod.jpg', N'Một không gian làm việc tiêu chuẩn với đầy đủ các tính năng cần thiết', 4, N'Còn trống', 3, 2),
(21, N'Pod Sáng Tạo', 'creative_pod.jpg', N'Không gian thiết kế độc đáo, truyền cảm hứng sáng tạo', 4, N'Còn trống', 3, 2),

--POD phòng họp
(22, N'Pod Cao cấp', 'premium_pod.jpg', N'Một không gian làm việc sang trọng với các tiện nghi cao cấp', 5, N'Còn trống', 4, 2),
(23, N'Pod Tiêu Chuẩn', 'standard_pod.jpg', N'Một không gian làm việc tiêu chuẩn với đầy đủ các tính năng cần thiết', 4, N'Còn trống', 4, 2),
(24, N'Pod Sáng Tạo', 'creative_pod.jpg', N'Không gian thiết kế độc đáo, truyền cảm hứng sáng tạo', 4, N'Còn trống', 4, 2);


--Insert into Utility
INSERT INTO [Utility] (Id, Name, Image, Description) VALUES
(1, N'Ổ cắm điện', 'ocamdien.jpg', N'Cung cấp thêm ổ cắm cho các thiết bị điện tử'),
(2, N'Máy chiếu', 'maychieu.jpg', N'Dành cho các buổi thuyết trình và họp'),
(3, N'Máy pha cà phê', 'mayphacaphe.jpg', N'Có cà phê miễn phí'),
(4, N'Hệ thống âm thanh', 'hethongamthanh.jpg', N' m thanh chất lượng cao cho các buổi họp'),
(5, N'Bảng trắng thông minh', 'bangtrangthongminh.jpg', N'Bảng viết điện tử cho các buổi thảo luận và họp');


--Insert into UtilityPod
INSERT INTO [UtilityPod] (UtilityId, PodId) VALUES 

--Tất cả loại phòng đều có thể có ổ điện
(1, 1),
(1, 2),
(1, 3),
(1, 4),
(1, 5),
(1, 6),
(1, 7),
(1, 8),
(1, 9),
(1, 10),
(1, 11),
(1, 12),
(1, 13),
(1, 14),
(1, 15),
(1, 16),
(1, 17),
(1, 18),
(1, 19),
(1, 20),
(1, 21),
(1, 22),
(1, 23),
(1, 24),

--Chỉ phòng họp có máy chiếu
(2, 10),
(2, 11),
(2, 12),
(2, 22),
(2, 23),
(2, 24),

--Chỉ phòng họp có máy pha cà phê
(3, 10),
(3, 11),
(3, 12),
(3, 22),
(3, 23),
(3, 24),

--Chỉ phòng họp có hệ thống âm thanh
(4, 10),
(4, 11),
(4, 12),
(4, 22),
(4, 23),
(4, 24),

--Chỉ phòng họp và POD nhóm bảng trắng thông minh

--Phòng họp
(5, 10),
(5, 11),
(5, 12),
(5, 22),
(5, 23),
(5, 24),

--Nhóm
(5, 7),
(5, 8),
(5, 9),
(5, 19),
(5, 20),
(5, 21);


--Insert into User
INSERT INTO [User] (Id, Email, Password, Name, Image, Role, Type, PhoneNumber, Point, Description) VALUES
(1, 'nguyen.van.an@gmail.com', 'User1', N'Nguyễn Văn An', 'nguyen.jpg', 'User', 'VIP', '0123456789', 10000, N'Khách hàng ưu tiên'),
(2, 'tran.bao@gmail.com', 'User2', N'Trần Thiên Bảo', 'tran.jpg', 'User', 'Regular', N'0987654321', 500, N'Khách hàng mới'),
(3, 'le.quang.hai@gmail.com', 'User003', N'Lê Quang Hải', 'le.jpg', 'User', 'VIP', N'0912345678', 10500, N'Khách hàng ưu tiên'),
(4, 'pham.thanh.dat@gmail.com', 'User004', N'Phạm Thành Đạt', 'pham.jpg', 'User', 'Regular', N'0934567890', 7000, N'Khách hàng tiềm năng'),
(5, 'hoang.van.chuong@gmail.com', 'User005', N'Hoàng Văn Chương', 'hoang.jpg', 'User', 'VIP', N'0945678901', 12000, N'Khách hàng ưu tiên'),
(6, 'vo.thi.tuyet@gmail.com', 'User006', N'Võ Thị Tuyết', 'vo.jpg', 'User', 'Regular', N'0956789012', 5500, N'Khách hàng cũ'),
(7, 'nguyen.huu.giang@gmail.com', 'User007', N'Nguyễn Hữu Giang', 'nguyen_huu.jpg', 'User', 'VIP', N'0967890123', 12500, N'Khách hàng ưu tiên'),
(8, 'do.thanh.ha@gmail.com', 'User008', N'Đỗ Thanh Hà', 'thang.jpg', 'User', 'Regular', N'0978901234', 1000, N'Khách hàng mới'),

(9, 'do.thanh.ha@gmail.com', 'User009', N'Đặng Ngọc Hải Triều', 'trieu.jpg', 'Admin', 'VIP', N'0978901234', 10000, N'Nhà điều hành'),
(10, 'do.thanh.ha@gmail.com', 'User010', N'Trương Kim Hằng', 'hang.jpg', 'Staff', 'VIP', N'0978901234', 10000, N'Quản lý'),
(11, 'do.thanh.ha@gmail.com', 'User011', N'Phạm Thành Danh', 'danh.jpg', 'Staff', 'VIP', N'0978901234', 10000, N'Quản lý'),
(12, 'do.thanh.ha@gmail.com', 'User012', N'Nguyễn Thành Dương', 'duong.jpg', 'Staff', 'VIP', N'0978901234', 10000, N'Quản lý');


--Insert into Slot
INSERT INTO [Slot] (Id, Name, StartTime, EndTime, Price, Status, PodId) VALUES

--Slot 1
(1, N'Slot 1', 7, 9, 80000, N'Còn trống', 1),
(2, N'Slot 1', 7, 9, 70000, N'Còn trống', 2),
(3, N'Slot 1', 7, 9, 60000, N'Còn trống', 3),
(4, N'Slot 1', 7, 9, 100000, N'Còn trống', 4),
(5, N'Slot 1', 7, 9, 90000, N'Còn trống', 5),
(6, N'Slot 1', 7, 9, 80000, N'Còn trống', 6),
(7, N'Slot 1', 7, 9, 120000, N'Còn trống', 7),
(8, N'Slot 1', 7, 9, 110000, N'Còn trống', 8),
(9, N'Slot 1', 7, 9, 100000, N'Còn trống', 9),
(10, N'Slot 1', 7, 9, 200000, N'Còn trống', 10),
(11, N'Slot 1', 7, 9, 190000, N'Còn trống', 11),
(12, N'Slot 1', 7, 9, 180000, N'Còn trống', 12),

(13, N'Slot 1', 7, 9, 80000, N'Còn trống', 13),
(14, N'Slot 1', 7, 9, 70000, N'Còn trống', 14),
(15, N'Slot 1', 7, 9, 60000, N'Còn trống', 15),
(16, N'Slot 1', 7, 9, 100000, N'Còn trống', 16),
(17, N'Slot 1', 7, 9, 90000, N'Còn trống', 17),
(18, N'Slot 1', 7, 9, 80000, N'Còn trống', 18),
(19, N'Slot 1', 7, 9, 120000, N'Còn trống', 19),
(20, N'Slot 1', 7, 9, 110000, N'Còn trống', 20),
(21, N'Slot 1', 7, 9, 100000, N'Còn trống', 21),
(22, N'Slot 1', 7, 9, 200000, N'Còn trống', 22),
(23, N'Slot 1', 7, 9, 190000, N'Còn trống', 23),
(24, N'Slot 1', 7, 9, 180000, N'Còn trống', 24),

--Slot 2
(25, N'Slot 2', 10, 12, 80000, N'Còn trống', 1),
(26, N'Slot 2', 10, 12, 70000, N'Còn trống', 2),
(27, N'Slot 2', 10, 12, 60000, N'Còn trống', 3),
(28, N'Slot 2', 10, 12, 100000, N'Còn trống', 4),
(29, N'Slot 2', 10, 12, 90000, N'Còn trống', 5),
(30, N'Slot 2', 10, 12, 80000, N'Còn trống', 6),
(31, N'Slot 2', 10, 12, 120000, N'Còn trống', 7),
(32, N'Slot 2', 10, 12, 110000, N'Còn trống', 8),
(33, N'Slot 2', 10, 12, 100000, N'Còn trống', 9),
(34, N'Slot 2', 10, 12, 200000, N'Còn trống', 10),
(35, N'Slot 2', 10, 12, 190000, N'Còn trống', 11),
(36, N'Slot 2', 10, 12, 180000, N'Còn trống', 12),

(37, N'Slot 2', 10, 12, 80000, N'Còn trống', 13),
(38, N'Slot 2', 10, 12, 70000, N'Còn trống', 14),
(39, N'Slot 2', 10, 12, 60000, N'Còn trống', 15),
(40, N'Slot 2', 10, 12, 100000, N'Còn trống', 16),
(41, N'Slot 2', 10, 12, 90000, N'Còn trống', 17),
(42, N'Slot 2', 10, 12, 80000, N'Còn trống', 18),
(43, N'Slot 2', 10, 12, 120000, N'Còn trống', 19),
(44, N'Slot 2', 10, 12, 110000, N'Còn trống', 20),
(45, N'Slot 2', 10, 12, 100000, N'Còn trống', 21),
(46, N'Slot 2', 10, 12, 200000, N'Còn trống', 22),
(47, N'Slot 2', 10, 12, 190000, N'Còn trống', 23),
(48, N'Slot 2', 10, 12, 180000, N'Còn trống', 24),

--Slot 3
(49, N'Slot 3', 13, 15, 80000, N'Còn trống', 1),
(50, N'Slot 3', 13, 15, 70000, N'Còn trống', 2),
(51, N'Slot 3', 13, 15, 60000, N'Còn trống', 3),
(52, N'Slot 3', 13, 15, 100000, N'Còn trống', 4),
(53, N'Slot 3', 13, 15, 90000, N'Còn trống', 5),
(54, N'Slot 3', 13, 15, 80000, N'Còn trống', 6),
(55, N'Slot 3', 13, 15, 120000, N'Còn trống', 7),
(56, N'Slot 3', 13, 15, 110000, N'Còn trống', 8),
(57, N'Slot 3', 13, 15, 100000, N'Còn trống', 9),
(58, N'Slot 3', 13, 15, 200000, N'Còn trống', 10),
(59, N'Slot 3', 13, 15, 190000, N'Còn trống', 11),
(60, N'Slot 3', 13, 15, 180000, N'Còn trống', 12),

(61, N'Slot 3', 13, 15, 80000, N'Còn trống', 13),
(62, N'Slot 3', 13, 15, 70000, N'Còn trống', 14),
(63, N'Slot 3', 13, 15, 60000, N'Còn trống', 15),
(64, N'Slot 3', 13, 15, 100000, N'Còn trống', 16),
(65, N'Slot 3', 13, 15, 90000, N'Còn trống', 17),
(66, N'Slot 3', 13, 15, 80000, N'Còn trống', 18),
(67, N'Slot 3', 13, 15, 120000, N'Còn trống', 19),
(68, N'Slot 3', 13, 15, 110000, N'Còn trống', 20),
(69, N'Slot 3', 13, 15, 100000, N'Còn trống', 21),
(70, N'Slot 3', 13, 15, 200000, N'Còn trống', 22),
(71, N'Slot 3', 13, 15, 190000, N'Còn trống', 23),
(72, N'Slot 3', 13, 15, 180000, N'Còn trống', 24),

--Slot 4
(73, N'Slot 4', 16, 18, 80000, N'Còn trống', 1),
(74, N'Slot 4', 16, 18, 70000, N'Còn trống', 2),
(75, N'Slot 4', 16, 18, 60000, N'Còn trống', 3),
(76, N'Slot 4', 16, 18, 100000, N'Còn trống', 4),
(77, N'Slot 4', 16, 18, 90000, N'Còn trống', 5),
(78, N'Slot 4', 16, 18, 80000, N'Còn trống', 6),
(79, N'Slot 4', 16, 18, 120000, N'Còn trống', 7),
(80, N'Slot 4', 16, 18, 110000, N'Còn trống', 8),
(81, N'Slot 4', 16, 18, 100000, N'Còn trống', 9),
(82, N'Slot 4', 16, 18, 200000, N'Còn trống', 10),
(83, N'Slot 4', 16, 18, 190000, N'Còn trống', 11),
(84, N'Slot 4', 16, 18, 180000, N'Còn trống', 12),

(85, N'Slot 4', 16, 18, 80000, N'Còn trống', 13),
(86, N'Slot 4', 16, 18, 70000, N'Còn trống', 14),
(87, N'Slot 4', 16, 18, 60000, N'Còn trống', 15),
(88, N'Slot 4', 16, 18, 100000, N'Còn trống', 16),
(89, N'Slot 4', 16, 18, 90000, N'Còn trống', 17),
(90, N'Slot 4', 16, 18, 80000, N'Còn trống', 18),
(91, N'Slot 4', 16, 18, 120000, N'Còn trống', 19),
(92, N'Slot 4', 16, 18, 110000, N'Còn trống', 20),
(93, N'Slot 4', 16, 18, 100000, N'Còn trống', 21),
(94, N'Slot 4', 16, 18, 200000, N'Còn trống', 22),
(95, N'Slot 4', 16, 18, 190000, N'Còn trống', 23),
(96, N'Slot 4', 16, 18, 180000, N'Còn trống', 24),

--Slot 5
(97, N'Slot 5', 19, 21, 80000, N'Còn trống', 1),
(98, N'Slot 5', 19, 21, 70000, N'Còn trống', 2),
(99, N'Slot 5', 19, 21, 60000, N'Còn trống', 3),
(100, N'Slot 5', 19, 21, 100000, N'Còn trống', 4),
(101, N'Slot 5', 19, 21, 90000, N'Còn trống', 5),
(102, N'Slot 5', 19, 21, 80000, N'Còn trống', 6),
(103, N'Slot 5', 19, 21, 120000, N'Còn trống', 7),
(104, N'Slot 5', 19, 21, 110000, N'Còn trống', 8),
(105, N'Slot 5', 19, 21, 100000, N'Còn trống', 9),
(106, N'Slot 5', 19, 21, 200000, N'Còn trống', 10),
(107, N'Slot 5', 19, 21, 190000, N'Còn trống', 11),
(108, N'Slot 5', 19, 21, 180000, N'Còn trống', 12),

(109, N'Slot 5', 19, 21, 80000, N'Còn trống', 13),
(110, N'Slot 5', 19, 21, 70000, N'Còn trống', 14),
(111, N'Slot 5', 19, 21, 60000, N'Còn trống', 15),
(112, N'Slot 5', 19, 21, 100000, N'Còn trống', 16),
(113, N'Slot 5', 19, 21, 90000, N'Còn trống', 17),
(114, N'Slot 5', 19, 21, 80000, N'Còn trống', 18),
(115, N'Slot 5', 19, 21, 120000, N'Còn trống', 19),
(116, N'Slot 5', 19, 21, 110000, N'Còn trống', 20),
(117, N'Slot 5', 19, 21, 100000, N'Còn trống', 21),
(118, N'Slot 5', 19, 21, 200000, N'Còn trống', 22),
(119, N'Slot 5', 19, 21, 190000, N'Còn trống', 23),
(120, N'Slot 5', 19, 21, 180000, N'Còn trống', 24),

--Slot 6
(121, N'Slot 6', 22, 24, 80000, N'Còn trống', 1),
(122, N'Slot 6', 22, 24, 70000, N'Còn trống', 2),
(123, N'Slot 6', 22, 24, 60000, N'Còn trống', 3),
(124, N'Slot 6', 22, 24, 100000, N'Còn trống', 4),
(125, N'Slot 6', 22, 24, 90000, N'Còn trống', 5),
(126, N'Slot 6', 22, 24, 80000, N'Còn trống', 6),
(127, N'Slot 6', 22, 24, 120000, N'Còn trống', 7),
(128, N'Slot 6', 22, 24, 110000, N'Còn trống', 8),
(129, N'Slot 6', 22, 24, 100000, N'Còn trống', 9),
(130, N'Slot 6', 22, 24, 200000, N'Còn trống', 10),
(131, N'Slot 6', 22, 24, 190000, N'Còn trống', 11),
(132, N'Slot 6', 22, 24, 180000, N'Còn trống', 12),

(133, N'Slot 6', 22, 24, 80000, N'Còn trống', 13),
(134, N'Slot 6', 22, 24, 70000, N'Còn trống', 14),
(135, N'Slot 6', 22, 24, 60000, N'Còn trống', 15),
(136, N'Slot 6', 22, 24, 100000, N'Còn trống', 16),
(137, N'Slot 6', 22, 24, 90000, N'Còn trống', 17),
(138, N'Slot 6', 22, 24, 80000, N'Còn trống', 18),
(139, N'Slot 6', 22, 24, 120000, N'Còn trống', 19),
(140, N'Slot 6', 22, 24, 110000, N'Còn trống', 20),
(141, N'Slot 6', 22, 24, 100000, N'Còn trống', 21),
(142, N'Slot 6', 22, 24, 200000, N'Còn trống', 22),
(143, N'Slot 6', 22, 24, 190000, N'Còn trống', 23),
(144, N'Slot 6', 22, 24, 180000, N'Còn trống', 24);


--Insert into Booking
INSERT INTO [Booking] (Id, Date, Status, Feedback, PodId, UserId) VALUES

--Đơn
(1, '2024-10-05', N'Xác nhận', N'Dịch vụ tuyệt vời', 1, 3),
(2, '2024-10-06', N'Xác nhận', NULL, 1, 4),
--Đôi
(3, '2024-10-07', N'Xác nhận', N'Mọi thứ đều ổn', 6, 5),
--Nhóm
(4, '2024-10-08', N'Đang chờ', NULL, 8, 6),
--Phòng họp
(5, '2024-10-09', N'Xác nhận', N'Rất hài lòng', 15, 7),

--Đơn
(6, '2024-10-10', N'Xác nhận', N'Dịch vụ tốt', 14, 8),
--Đôi
(7, '2024-10-11', N'Xác nhận', NULL, 17, 1),
--Nhóm
(8, '2024-10-12', N'Đang chờ', NULL, 19, 2),
(9, '2024-10-13', N'Xác nhận', N'Sẽ quay lại', 19, 3),
--Phòng họp
(10, '2024-10-14', N'Xác nhận', N'Tuyệt vời!', 22, 4);


--Insert into SlotBooking
INSERT INTO [SlotBooking] (SlotId, BookingId) VALUES

--Đơn
(1, 1),
(25, 2),
--Đôi
(6, 3),
--Nhóm
(8, 4),
--Phòng họp
(15, 5),

--Đơn
(14, 6),
--Đôi
(17, 7),
--Nhóm
(19, 8),
(43, 9),
--Phòng họp
(22, 10);


--Insert into Payment
INSERT INTO [Payment] (Id, Method, Amount, Date, Status, BookingId) VALUES
(1, N'Thanh toán qua Momo', 120000, '2024-10-05', N'Đã thanh toán', 1),
(2, N'Thanh toán qua Momo', 80000, '2024-10-06', N'Đã thanh toán', 2),
(3, N'Thanh toán qua VNPay', 80000, '2024-10-07', N' Đã thanh toán', 3),
(4, N'Thanh toán trực tiếp bằng tiền mặt', 400000, '2024-10-08', N'Chưa thanh toán', 4),
(5, N'Thanh toán qua Momo', 60000, '2024-10-09', N'Đã thanh toán', 5),
(6, N'Thanh toán qua VNPay', 70000, '2024-10-10', N'Đã thanh toán', 6),
(7, N'Thanh toán bằng tiền mặt', 190000, '2024-10-11', N'Đã thanh toán', 7),
(8, N'Thanh toán bằng tiền mặt', 120000, '2024-10-12', N'Chưa thanh toán', 8),
(9, N'Thanh toán qua VNPay', 120000, '2024-10-13', N'Đã thanh toán', 9), 
(10, N'Thanh toán trực tiếp bằng tiền mặt', 400000, '2024-10-14', N'Đã thanh toán', 10);


--Insert into Category
INSERT INTO [Category] (Id, Name, Status) VALUES
(1, N'Đồ ăn', N'Vẫn còn'),
(2, N'Đồ uống', N'Vẫn còn'),
(3, N'Đồ chơi', N'Vẫn còn');


--Insert into Product
INSERT INTO [Product] (Id, Name, Price, Description, Rating, Stock, StoreId, CategoryId) VALUES

--Store 1
(1, N'Bánh quy', 20000, N'Bánh quy giòn tan', 4, 56, 1, 1),
(2, N'Bánh bim bim', 10000, N'Các loại oishi ngon', 5, 80, 1, 1),
(3, N'Kẹo', 15000, N'Kẹo ngọt cho mọi người', 4, 300, 1, 1),
(4, N'Trái cây', 25000, N'Trái cây tươi mát', 4, 10, 1, 1),
(5, N'Burger', 60000, N'Burger thịt bò tươi ngon', 4, 150, 1, 1),
(6, N'Fries', 30000, N'Khoai tây chiên giòn', 4, 200, 1, 1),

(7, N'Nước lọc', 10000, N'Nước lọc tinh khiết', 5, 200, 1, 2),
(8, N'Nước ngọt', 20000, N'Nước ngọt có gas', 4, 300, 1, 2),
(9, N'Tea lạnh', 10000, N'Tea lạnh giải khát', 5, 150, 1, 2),
(10, N'Cà phê', 40000, N'Cà phê rang xay nguyên chất', 5, 120, 1, 2),
(11, N'Sinh tố', 40000, N'Sinh tố trái cây mát lạnh', 4, 90, 1, 2),
(12, N'Trà sữa', 40000, N'Trà sữa truyền thống', 5, 220, 1, 2),

(13, N'Cờ cá ngựa', 10000, N'Đua cờ cá ngựa', 5, 10, 1, 3),
(14, N'Cờ vua', 10000, N'Cờ vua chiến thuật', 5, 10, 1, 3),
(15, N'Bài UNO', 10000, N'Bài UNO đầy đủ chức năng', 5, 10, 1, 3),
(16, N'Bài ma sói', 10000, N'Đứa nào là sói?', 5, 10, 1, 3),
(17, N'Xếp gỗ', 10000, N'Xếp gỗ thành tháp', 5, 10, 1, 3),
(18, N'Detective Conan', 10000, N'Truyện tranh Conan', 4, 200, 1, 3),

--Store 2
(19, N'Bánh quy', 20000, N'Bánh quy giòn tan', 4, 100, 2, 1),
(20, N'Bánh bim bim', 10000, N'Các loại oishi ngon', 5, 80, 2, 1),
(21, N'Kẹo', 15000, N'Kẹo ngọt cho mọi người', 4, 300, 2, 1),
(22, N'Trái cây', 25000, N'Trái cây tươi mát', 4, 10, 2, 1),
(23, N'Burger', 60000, N'Burger thịt bò tươi ngon', 4, 150, 2, 1),
(24, N'Fries', 30000, N'Khoai tây chiên giòn', 4, 200, 2, 1),

(25, N'Nước lọc', 10000, N'Nước lọc tinh khiết', 5, 200, 2, 2),
(26, N'Nước ngọt', 20000, N'Nước ngọt có gas', 4, 300, 2, 2),
(27, N'Tea lạnh', 10000, N'Tea lạnh giải khát', 5, 150, 2, 2),
(28, N'Cà phê', 40000, N'Cà phê rang xay nguyên chất', 5, 120, 2, 2),
(29, N'Sinh tố', 40000, N'Sinh tố trái cây mát lạnh', 4, 90, 2, 2),
(30, N'Trà sữa', 40000, N'Trà sữa truyền thống', 5, 220, 2, 2),

(31, N'Cờ cá ngựa', 10000, N'Đua cờ cá ngựa', 5, 10, 2, 3),
(32, N'Cờ vua', 10000, N'Cờ vua chiến thuật', 5, 10, 2, 3),
(33, N'Bài UNO', 10000, N'Bài UNO đầy đủ chức năng', 5, 10, 2, 3),
(34, N'Bài ma sói', 10000, N'Đứa nào là sói?', 5, 10, 2, 3),
(35, N'Xếp gỗ', 10000, N'Xếp gỗ thành tháp', 5, 10, 2, 3),
(36, N'Detective Conan', 10000, N'Truyện tranh Conan', 4, 200, 2, 3);


--Insert into BookingOrder
INSERT INTO [BookingOrder] (Id, Amount, Quantity, Status, Date, BookingId, ProductId) VALUES

--Đơn
(1, 40000, 1, N'Đã thanh toán', '2024-10-05', 1, 11),  --Sinh tố

--Đôi
(2, 80000, 2, N'Đã thanh toán', '2024-10-11', 7, 28),  --Cà phê
(3, 20000, 1, N'Đã thanh toán', '2024-10-11', 7, 19),  --Bánh quy

--Nhóm
(4, 10000, 1, N'Chưa thanh toán', '2024-10-08', 4, 15),  --Bài UNO
(5, 240000, 6, N'Chưa thanh toán', '2024-10-08', 4, 12),  --Trà sữa
(6, 40000, 4, N'Chưa thanh toán', '2024-10-08', 4, 2),  --Bánh bim bim

--Phòng họp
(7, 100000, 5, N'Đã thanh toán', '2024-10-14', 10, 19),  --Bánh quy
(8, 100000, 10, N'Đã thanh toán', '2024-10-14', 10, 27);  --Tea lạnh
