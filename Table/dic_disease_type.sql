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

Date: 2018-06-08 09:26:29
*/


-- ----------------------------
-- Table structure for dic_disease_type
-- ----------------------------
DROP TABLE IF EXISTS "public"."dic_disease_type";
CREATE TABLE "public"."dic_disease_type" (
"id" int2 DEFAULT nextval('dic_disease_type_id_seq'::regclass) NOT NULL,
"disease_type" varchar(255) COLLATE "default" DEFAULT ''::character varying NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."dic_disease_type" IS '病害类型表';
COMMENT ON COLUMN "public"."dic_disease_type"."disease_type" IS '病害类型';

-- ----------------------------
-- Records of dic_disease_type
-- ----------------------------
INSERT INTO "public"."dic_disease_type" VALUES ('1', '无');
INSERT INTO "public"."dic_disease_type" VALUES ('2', '烂根病');
INSERT INTO "public"."dic_disease_type" VALUES ('3', '白粉病');
INSERT INTO "public"."dic_disease_type" VALUES ('4', '条锈病');
INSERT INTO "public"."dic_disease_type" VALUES ('5', '未知');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table dic_disease_type
-- ----------------------------
ALTER TABLE "public"."dic_disease_type" ADD PRIMARY KEY ("id");
