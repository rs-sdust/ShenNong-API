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

Date: 2018-06-08 09:26:20
*/


-- ----------------------------
-- Table structure for dic_crop_type
-- ----------------------------
DROP TABLE IF EXISTS "public"."dic_crop_type";
CREATE TABLE "public"."dic_crop_type" (
"id" int2 DEFAULT nextval('dic_crop_type_id_seq'::regclass) NOT NULL,
"crop_type" varchar(10) COLLATE "default" DEFAULT ''::character varying NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."dic_crop_type" IS '作物类型字典表';

-- ----------------------------
-- Records of dic_crop_type
-- ----------------------------
INSERT INTO "public"."dic_crop_type" VALUES ('1', '冬小麦');
INSERT INTO "public"."dic_crop_type" VALUES ('2', '春小麦');
INSERT INTO "public"."dic_crop_type" VALUES ('3', '夏玉米');
INSERT INTO "public"."dic_crop_type" VALUES ('4', '早稻');
INSERT INTO "public"."dic_crop_type" VALUES ('5', '中稻');
INSERT INTO "public"."dic_crop_type" VALUES ('6', '晚稻');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table dic_crop_type
-- ----------------------------
ALTER TABLE "public"."dic_crop_type" ADD PRIMARY KEY ("id");
