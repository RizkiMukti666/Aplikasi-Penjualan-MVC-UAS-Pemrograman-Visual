-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:33000
-- Generation Time: Jan 04, 2026 at 09:10 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dbpenjualan`
--

-- --------------------------------------------------------

--
-- Table structure for table `category`
--

CREATE TABLE `category` (
  `id` int(11) NOT NULL,
  `categoryDesc` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `category`
--

INSERT INTO `category` (`id`, `categoryDesc`) VALUES
(1, 'ATK'),
(2, 'Sembako'),
(3, 'Peralatan Dapur'),
(4, 'Elektronik'),
(5, 'Alat Mandi'),
(6, 'Perkakas'),
(7, 'Dekorasi'),
(8, 'frree');

-- --------------------------------------------------------

--
-- Table structure for table `items`
--

CREATE TABLE `items` (
  `id` int(11) NOT NULL,
  `itemID` varchar(12) NOT NULL,
  `itemDesc` varchar(100) NOT NULL,
  `itemCate` int(11) NOT NULL,
  `unit` varchar(15) NOT NULL,
  `salesPrice` int(11) NOT NULL,
  `minStock` int(11) NOT NULL,
  `stock` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `items`
--

INSERT INTO `items` (`id`, `itemID`, `itemDesc`, `itemCate`, `unit`, `salesPrice`, `minStock`, `stock`) VALUES
(1, 'B0001', 'Buku Tulis', 1, 'Pcs', 5000, 10, 105),
(2, 'B0002', 'Pemghapus', 1, 'Pcs', 1500, 20, 110),
(5, 'B0004', 'Pensil', 1, 'Pcs', 4000, 20, 100),
(6, 'B0005', 'Lampu', 4, 'Pcs', 14000, 20, 104),
(7, 'B0006', 'Penggaris', 1, 'Pcs', 8000, 10, 3),
(8, 'B0007', 'Tipe X', 1, 'Pcs', 7000, 20, 19),
(9, 'B0008', 'Baterai AAA', 4, 'Pcs', 4000, 20, 40),
(10, 'B0009', 'Figura Foto', 7, 'Pcs', 23000, 10, 20),
(11, 'B0010', 'Baterai AA', 4, 'Pcs', 5000, 20, 30),
(12, 'B0011', 'Terminal 5 Meter', 4, 'Pcs', 28000, 10, 15);

-- --------------------------------------------------------

--
-- Table structure for table `mahasiswa`
--

CREATE TABLE `mahasiswa` (
  `id` int(11) NOT NULL,
  `nim` varchar(12) NOT NULL,
  `nama` varchar(60) NOT NULL,
  `jurusan` varchar(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `mahasiswa`
--

INSERT INTO `mahasiswa` (`id`, `nim`, `nama`, `jurusan`) VALUES
(1, '111', 'heri', 'dd'),
(2, '333', 'dede', 'dd'),
(3, '666', 'desi', 'dd');

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `id` int(11) NOT NULL,
  `kode` varchar(5) NOT NULL,
  `produk` varchar(100) NOT NULL,
  `harga` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `purchase`
--

CREATE TABLE `purchase` (
  `idPurchase` varchar(12) NOT NULL,
  `purchaseDate` datetime NOT NULL,
  `supplierID` int(11) NOT NULL,
  `totalAmount` int(11) NOT NULL DEFAULT 0,
  `notes` varchar(255) DEFAULT NULL,
  `createdBy` int(11) DEFAULT NULL,
  `createdDate` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `purchase`
--

INSERT INTO `purchase` (`idPurchase`, `purchaseDate`, `supplierID`, `totalAmount`, `notes`, `createdBy`, `createdDate`) VALUES
('PUR0001', '2025-12-30 16:26:07', 2, 71000, NULL, 1, '2025-12-30 16:26:49'),
('PUR0002', '2026-01-04 10:42:52', 1, 57000, NULL, 1, '2026-01-04 10:43:30'),
('PUR0003', '2026-01-04 10:58:35', 2, 1330000, NULL, 1, '2026-01-04 10:59:21');

-- --------------------------------------------------------

--
-- Table structure for table `purchasedetail`
--

CREATE TABLE `purchasedetail` (
  `id` int(11) NOT NULL,
  `idPurchase` varchar(12) NOT NULL,
  `itemID` int(11) NOT NULL,
  `qtyPurchase` int(11) NOT NULL,
  `purchasePrice` int(11) NOT NULL,
  `subtotal` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `purchasedetail`
--

INSERT INTO `purchasedetail` (`id`, `idPurchase`, `itemID`, `qtyPurchase`, `purchasePrice`, `subtotal`) VALUES
(1, 'PUR0001', 6, 4, 14000, 56000),
(2, 'PUR0001', 2, 10, 1500, 15000),
(3, 'PUR0002', 7, 4, 8000, 32000),
(4, 'PUR0002', 1, 5, 5000, 25000),
(5, 'PUR0003', 8, 20, 7000, 140000),
(6, 'PUR0003', 9, 40, 4000, 160000),
(7, 'PUR0003', 10, 20, 23000, 460000),
(8, 'PUR0003', 11, 30, 5000, 150000),
(9, 'PUR0003', 12, 15, 28000, 420000);

-- --------------------------------------------------------

--
-- Table structure for table `sale`
--

CREATE TABLE `sale` (
  `idTrans` varchar(12) NOT NULL,
  `saleDate` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `sale`
--

INSERT INTO `sale` (`idTrans`, `saleDate`) VALUES
('TRX0001', '2025-12-17 20:22:51'),
('TRX0002', '2025-12-17 20:28:28'),
('TRX0003', '2025-12-17 20:28:50'),
('TRX0004', '2025-12-17 20:51:26'),
('TRX0005', '2025-12-17 20:51:57'),
('TRX0006', '2025-12-24 15:15:31'),
('TRX0007', '2025-12-24 21:17:35'),
('TRX0008', '2025-12-30 15:01:16'),
('TRX0009', '2026-01-04 11:05:17');

-- --------------------------------------------------------

--
-- Table structure for table `saledetail`
--

CREATE TABLE `saledetail` (
  `id` int(11) NOT NULL,
  `idSale` varchar(12) NOT NULL,
  `itemID` int(11) NOT NULL,
  `qtySale` int(11) NOT NULL,
  `price` int(11) NOT NULL,
  `subtotal` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `saledetail`
--

INSERT INTO `saledetail` (`id`, `idSale`, `itemID`, `qtySale`, `price`, `subtotal`) VALUES
(4, 'TRX0001', 1, 1, 5000, 5000),
(5, 'TRX0002', 1, 3, 5000, 15000),
(6, 'TRX0003', 1, 1, 5000, 5000),
(7, 'TRX0003', 2, 1, 1500, 1500),
(8, 'TRX0004', 1, 1, 5000, 5000),
(9, 'TRX0004', 2, 1, 1500, 1500),
(10, 'TRX0005', 1, 5, 5000, 25000),
(11, 'TRX0005', 2, 3, 1500, 4500),
(12, 'TRX0006', 2, 1, 1500, 1500),
(13, 'TRX0007', 2, 1, 1500, 1500),
(14, 'TRX0007', 1, 1, 5000, 5000),
(15, 'TRX0008', 2, 4, 1500, 6000),
(16, 'TRX0008', 5, 5, 4000, 20000),
(17, 'TRX0008', 6, 3, 14000, 42000),
(18, 'TRX0009', 7, 1, 8000, 8000),
(19, 'TRX0009', 8, 1, 7000, 7000);

-- --------------------------------------------------------

--
-- Table structure for table `supplier`
--

CREATE TABLE `supplier` (
  `id` int(11) NOT NULL,
  `supplierID` varchar(12) NOT NULL,
  `supplierName` varchar(100) NOT NULL,
  `address` varchar(255) DEFAULT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `contactPerson` varchar(100) DEFAULT NULL,
  `isActive` tinyint(1) NOT NULL DEFAULT 1,
  `createdDate` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `supplier`
--

INSERT INTO `supplier` (`id`, `supplierID`, `supplierName`, `address`, `phone`, `email`, `contactPerson`, `isActive`, `createdDate`) VALUES
(1, 'SUP0001', 'PT. Sumber Makmur', 'Jl. Industri No. 10, Jakarta', '021-5551234', 'info@sumbermakmur.com', 'Budi Santoso', 1, '2025-12-30 16:11:33'),
(2, 'SUP0002', 'CV. Maju Jaya', 'Jl. Raya Bogor No. 25, Bogor', '0251-8881234', 'sales@majujaya.com', 'Dewi Lestari', 1, '2025-12-30 16:11:33');

-- --------------------------------------------------------

--
-- Table structure for table `tb_user`
--

CREATE TABLE `tb_user` (
  `id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(255) NOT NULL,
  `fullname` varchar(100) NOT NULL,
  `email` varchar(100) DEFAULT NULL,
  `role` varchar(20) NOT NULL DEFAULT 'user',
  `isActive` tinyint(1) NOT NULL DEFAULT 1,
  `createdDate` datetime NOT NULL DEFAULT current_timestamp(),
  `lastLogin` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `tb_user`
--

INSERT INTO `tb_user` (`id`, `username`, `password`, `fullname`, `email`, `role`, `isActive`, `createdDate`, `lastLogin`) VALUES
(1, 'admin', 'admin123', 'Administrator', 'admin@penjualan.com', 'admin', 1, '2025-12-17 20:50:15', '2026-01-04 14:10:59'),
(2, 'kasir', 'kasir123', 'Kasir Toko', 'kasir@penjualan.com', 'kasir', 1, '2025-12-17 20:50:15', NULL),
(3, 'user', 'user123', 'User Biasa', 'user@penjualan.com', 'user', 1, '2025-12-17 20:50:15', NULL);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `category`
--
ALTER TABLE `category`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `mahasiswa`
--
ALTER TABLE `mahasiswa`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `purchase`
--
ALTER TABLE `purchase`
  ADD PRIMARY KEY (`idPurchase`);

--
-- Indexes for table `purchasedetail`
--
ALTER TABLE `purchasedetail`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `sale`
--
ALTER TABLE `sale`
  ADD PRIMARY KEY (`idTrans`);

--
-- Indexes for table `saledetail`
--
ALTER TABLE `saledetail`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `supplier`
--
ALTER TABLE `supplier`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `supplierID` (`supplierID`);

--
-- Indexes for table `tb_user`
--
ALTER TABLE `tb_user`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `category`
--
ALTER TABLE `category`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `items`
--
ALTER TABLE `items`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `mahasiswa`
--
ALTER TABLE `mahasiswa`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `purchasedetail`
--
ALTER TABLE `purchasedetail`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `saledetail`
--
ALTER TABLE `saledetail`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT for table `supplier`
--
ALTER TABLE `supplier`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `tb_user`
--
ALTER TABLE `tb_user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
