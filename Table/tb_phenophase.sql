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

Date: 2018-05-15 14:23:05
*/


-- ----------------------------
-- Table structure for tb_phenophase
-- ----------------------------
DROP TABLE IF EXISTS "public"."tb_phenophase";
CREATE TABLE "public"."tb_phenophase" (
"id" int2 DEFAULT nextval('tb_phenophase_id_seq'::regclass) NOT NULL,
"crop_type" int4 DEFAULT '-1'::integer NOT NULL,
"phen_type" int4 DEFAULT '-1'::integer NOT NULL,
"time" int4 NOT NULL,
"phen_detail" varchar(255) COLLATE "default" DEFAULT ''::character varying NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."tb_phenophase" IS '作物物候信息表';

-- ----------------------------
-- Records of tb_phenophase
-- ----------------------------
INSERT INTO "public"."tb_phenophase" VALUES ('0', '1', '1', '20', '圣诞');
INSERT INTO "public"."tb_phenophase" VALUES ('1', '1', '2', '30', 'v从v地方v');
INSERT INTO "public"."tb_phenophase" VALUES ('2', '1', '3', '50', '对方热风');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tb_phenophase
-- ----------------------------
ALTER TABLE "public"."tb_phenophase" ADD PRIMARY KEY ("id");
