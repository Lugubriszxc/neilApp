-- phpMyAdmin SQL Dump
-- version 5.1.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 02, 2023 at 05:08 AM
-- Server version: 10.4.24-MariaDB
-- PHP Version: 7.4.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `prelimcrud`
--

-- --------------------------------------------------------

--
-- Table structure for table `category`
--

CREATE TABLE `category` (
  `id` int(11) NOT NULL,
  `name` varchar(500) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `category`
--

INSERT INTO `category` (`id`, `name`) VALUES
(1, 'Drug'),
(8, 'X'),
(9, 'hello'),
(11, 'Hello'),
(16, 'Canned goods'),
(18, 'd'),
(19, 'xx');

-- --------------------------------------------------------

--
-- Table structure for table `product`
--

CREATE TABLE `product` (
  `id` int(11) NOT NULL,
  `category` varchar(250) NOT NULL,
  `name` varchar(250) NOT NULL,
  `units` varchar(250) NOT NULL,
  `stock` int(11) NOT NULL,
  `price` varchar(250) NOT NULL,
  `status` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `product`
--

INSERT INTO `product` (`id`, `category`, `name`, `units`, `stock`, `price`, `status`) VALUES
(1, '1', 'asdasd', '23', 25, '60000', 'Active'),
(2, '1', 'Shabu', '23', 50, '6000', 'Active'),
(3, '11', 'Drug', '23', 24, '60000', 'Active'),
(7, '18', 'd', 'd', 28, '4', 'Inactive'),
(11, '1', 'tests', '4', 34, '5', 'Active');

-- --------------------------------------------------------

--
-- Table structure for table `stockhistory`
--

CREATE TABLE `stockhistory` (
  `a_Stock_ID` int(11) NOT NULL,
  `prodID` int(11) NOT NULL,
  `added_stock` int(11) NOT NULL,
  `date` varchar(500) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `stockhistory`
--

INSERT INTO `stockhistory` (`a_Stock_ID`, `prodID`, `added_stock`, `date`) VALUES
(2, 11, 5, '5/1/2023 9:42:12 PM'),
(3, 11, 3, '5/1/2023 10:32:53 PM'),
(4, 7, 2, '5/1/2023 11:24:03 PM'),
(5, 7, 3, '5/2/2023 10:08:49 AM'),
(6, 1, 2, '5/2/2023 10:09:02 AM'),
(7, 11, 2, '5/2/2023 10:35:25 AM'),
(10, 11, 1, '5/2/2023 10:42:24 AM'),
(11, 11, 1, '5/2/2023 10:43:49 AM'),
(12, 11, 1, '5/2/2023 10:44:25 AM'),
(13, 11, 1, '5/2/2023 10:47:37 AM'),
(14, 3, 1, '5/2/2023 11:07:15 AM');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `category`
--
ALTER TABLE `category`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `product`
--
ALTER TABLE `product`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `stockhistory`
--
ALTER TABLE `stockhistory`
  ADD PRIMARY KEY (`a_Stock_ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `category`
--
ALTER TABLE `category`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT for table `product`
--
ALTER TABLE `product`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `stockhistory`
--
ALTER TABLE `stockhistory`
  MODIFY `a_Stock_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
