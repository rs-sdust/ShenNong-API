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

Date: 2018-06-08 09:26:40
*/


-- ----------------------------
-- Table structure for dic_moisture_type
-- ----------------------------
DROP TABLE IF EXISTS "public"."dic_moisture_type";
CREATE TABLE "public"."dic_moisture_type" (
"id" int2 DEFAULT nextval('dic_moisture_type_id_seq'::regclass) NOT NULL,
"moisture_type" varchar(255) COLLATE "default" DEFAULT ''::character varying NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."dic_moisture_type" IS '土壤湿度类型表';
COMMENT ON COLUMN "public"."dic_moisture_type"."moisture_type" IS '土壤湿度类型';

-- ----------------------------
-- Records of dic_moisture_type
-- ----------------------------
INSERT INTO "public"."dic_moisture_type" VALUES ('1', '干旱');
INSERT INTO "public"."dic_moisture_type" VALUES ('2', '适宜');
INSERT INTO "public"."dic_moisture_type" VALUES ('3', '湿润');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table dic_moisture_type
-- ----------------------------
ALTER TABLE "public"."dic_moisture_type" ADD PRIMARY KEY ("id");
