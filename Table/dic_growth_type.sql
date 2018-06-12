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

Date: 2018-06-08 09:26:33
*/


-- ----------------------------
-- Table structure for dic_growth_type
-- ----------------------------
DROP TABLE IF EXISTS "public"."dic_growth_type";
CREATE TABLE "public"."dic_growth_type" (
"id" int2 DEFAULT nextval('dic_growth_type_id_seq'::regclass) NOT NULL,
"growth_type" varchar(255) COLLATE "default" NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."dic_growth_type" IS '作物长势表';
COMMENT ON COLUMN "public"."dic_growth_type"."growth_type" IS '作物长势';

-- ----------------------------
-- Records of dic_growth_type
-- ----------------------------
INSERT INTO "public"."dic_growth_type" VALUES ('1', '好');
INSERT INTO "public"."dic_growth_type" VALUES ('2', '中');
INSERT INTO "public"."dic_growth_type" VALUES ('3', ' 差');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table dic_growth_type
-- ----------------------------
ALTER TABLE "public"."dic_growth_type" ADD PRIMARY KEY ("id");
