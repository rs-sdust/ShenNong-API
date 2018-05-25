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

Date: 2018-05-15 14:22:58
*/


-- ----------------------------
-- Table structure for tb_market
-- ----------------------------
DROP TABLE IF EXISTS "public"."tb_market";
CREATE TABLE "public"."tb_market" (
"id" serial2 NOT NULL,
"crop_type" int4 DEFAULT '-1'::integer NOT NULL,
"crop_price" float8 DEFAULT 0 NOT NULL,
"time" date NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."tb_market" IS '市场行情表';

-- ----------------------------
-- Records of tb_market
-- ----------------------------
INSERT INTO "public"."tb_market" VALUES ('1', '1', '253.23', '2018-05-08');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tb_market
-- ----------------------------
ALTER TABLE "public"."tb_market" ADD PRIMARY KEY ("id");
