/*
Navicat PGSQL Data Transfer

Source Server         : 192.168.1.253_5432
Source Server Version : 100100
Source Host           : 192.168.1.253:5432
Source Database       : shennong
Source Schema         : public

Target Server Type    : PGSQL
Target Server Version : 100100
File Encoding         : 65001

Date: 2018-06-08 09:27:59
*/


-- ----------------------------
-- Table structure for tb_crop_growthday
-- ----------------------------
DROP TABLE IF EXISTS "public"."tb_crop_growthday";
CREATE TABLE "public"."tb_crop_growthday" (
"id" int2 DEFAULT nextval('crop_growth_day_id_seq'::regclass) NOT NULL,
"crop_type" int4 NOT NULL,
"growth_days" int4 NOT NULL
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of tb_crop_growthday
-- ----------------------------
INSERT INTO "public"."tb_crop_growthday" VALUES ('1', '1', '226');
INSERT INTO "public"."tb_crop_growthday" VALUES ('2', '2', '124');
INSERT INTO "public"."tb_crop_growthday" VALUES ('3', '3', '96');
INSERT INTO "public"."tb_crop_growthday" VALUES ('4', '4', '123');
INSERT INTO "public"."tb_crop_growthday" VALUES ('5', '5', '134');
INSERT INTO "public"."tb_crop_growthday" VALUES ('6', '6', '139');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tb_crop_growthday
-- ----------------------------
ALTER TABLE "public"."tb_crop_growthday" ADD PRIMARY KEY ("id");
