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

Date: 2018-06-08 09:26:54
*/


-- ----------------------------
-- Table structure for dic_pest_type
-- ----------------------------
DROP TABLE IF EXISTS "public"."dic_pest_type";
CREATE TABLE "public"."dic_pest_type" (
"id" int2 DEFAULT nextval('dic_pest_type_id_seq'::regclass) NOT NULL,
"pest_type" varchar(255) COLLATE "default" DEFAULT ''::character varying NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."dic_pest_type" IS '虫害类型表';
COMMENT ON COLUMN "public"."dic_pest_type"."pest_type" IS '虫害类型';

-- ----------------------------
-- Records of dic_pest_type
-- ----------------------------
INSERT INTO "public"."dic_pest_type" VALUES ('1', '无');
INSERT INTO "public"."dic_pest_type" VALUES ('2', '蚜虫');
INSERT INTO "public"."dic_pest_type" VALUES ('3', '吸浆虫');
INSERT INTO "public"."dic_pest_type" VALUES ('4', '未知');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table dic_pest_type
-- ----------------------------
ALTER TABLE "public"."dic_pest_type" ADD PRIMARY KEY ("id");
