/*
Navicat PGSQL Data Transfer

Source Server         : 192.168.2.253_5432
Source Server Version : 100100
Source Host           : 192.168.2.253:5432
Source Database       : shennong
Source Schema         : public

Target Server Type    : PGSQL
Target Server Version : 100100
File Encoding         : 65001

Date: 2018-05-15 14:22:25
*/


-- ----------------------------
-- Table structure for dic_rsi_type
-- ----------------------------
DROP TABLE IF EXISTS "public"."dic_rsi_type";
CREATE TABLE "public"."dic_rsi_type" (
"id" int2 DEFAULT nextval('dic_rsi_type_id_seq'::regclass) NOT NULL,
"rsi_type" varchar(10) COLLATE "default" DEFAULT ''::character varying NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."dic_rsi_type" IS '农情类型字典表';

-- ----------------------------
-- Records of dic_rsi_type
-- ----------------------------

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table dic_rsi_type
-- ----------------------------
ALTER TABLE "public"."dic_rsi_type" ADD PRIMARY KEY ("id");
