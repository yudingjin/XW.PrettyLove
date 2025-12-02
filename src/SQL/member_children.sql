/*
Navicat MySQL Data Transfer

Source Server         : development
Source Server Version : 50505
Source Host           : 192.168.0.34:3306
Source Database       : dev

Target Server Type    : MYSQL
Target Server Version : 50505
File Encoding         : 65001

Date: 2025-12-02 16:43:24
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `member_children`
-- ----------------------------
DROP TABLE IF EXISTS `member_children`;
CREATE TABLE `member_children` (
  `Id` bigint(20) NOT NULL,
  `CreatedTime` datetime(3) NOT NULL,
  `MemberId` bigint(20) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `MaritalStatus` smallint(6) NOT NULL,
  `Nationality` varchar(10) NOT NULL,
  `Hometown` varchar(100) NOT NULL,
  `Address` varchar(200) NOT NULL,
  `Occupation` varchar(20) NOT NULL,
  `House` varchar(100) NOT NULL,
  `VehicleInfo` varchar(100) NOT NULL,
  `BodyType` smallint(6) NOT NULL,
  `Gender` smallint(6) NOT NULL,
  `Education` smallint(6) NOT NULL,
  `IncomLevel` smallint(6) NOT NULL,
  `Height` int(11) NOT NULL,
  `Remark` varchar(500) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of member_children
-- ----------------------------
