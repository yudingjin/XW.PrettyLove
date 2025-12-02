/*
Navicat MySQL Data Transfer

Source Server         : development
Source Server Version : 50505
Source Host           : 192.168.0.34:3306
Source Database       : dev

Target Server Type    : MYSQL
Target Server Version : 50505
File Encoding         : 65001

Date: 2025-12-02 16:43:16
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `member`
-- ----------------------------
DROP TABLE IF EXISTS `member`;
CREATE TABLE `member` (
  `Id` bigint(20) NOT NULL,
  `CreatedTime` datetime(3) NOT NULL,
  `OpenId` varchar(50) NOT NULL,
  `NickName` varchar(50) NOT NULL,
  `AvatarUrl` varchar(200) NOT NULL,
  `Gender` smallint(6) NOT NULL,
  `Country` varchar(50) NOT NULL,
  `Province` varchar(50) NOT NULL,
  `City` varchar(50) NOT NULL,
  `Phone` varchar(20) NOT NULL,
  `LastLoginTime` datetime(3) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of member
-- ----------------------------
