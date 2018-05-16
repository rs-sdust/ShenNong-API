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

Date: 2018-05-15 14:22:46
*/


-- ----------------------------
-- Table structure for tb_field_rsi
-- ----------------------------
DROP TABLE IF EXISTS "public"."tb_field_rsi";
CREATE TABLE "public"."tb_field_rsi" (
"id" int2 DEFAULT nextval('tb_filed_rsi_id_seq'::regclass) NOT NULL,
"farm" int4 DEFAULT '-1'::integer NOT NULL,
"field" int4 DEFAULT '-1'::integer NOT NULL,
"type" int4 DEFAULT '-1'::integer NOT NULL,
"date" date NOT NULL,
"grade" int4 DEFAULT '-1'::integer NOT NULL,
"value" float8 DEFAULT 0 NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."tb_field_rsi" IS '遥感农情信息表';
COMMENT ON COLUMN "public"."tb_field_rsi"."field" IS 'FK_地块编号';
COMMENT ON COLUMN "public"."tb_field_rsi"."type" IS 'FK_农情类型';
COMMENT ON COLUMN "public"."tb_field_rsi"."grade" IS '产品数据分类后的等级';
COMMENT ON COLUMN "public"."tb_field_rsi"."value" IS '产品反演的实际数值';

-- ----------------------------
-- Records of tb_field_rsi
-- ----------------------------
INSERT INTO "public"."tb_field_rsi" VALUES ('0', '1', '1', '2', '2018-05-11', '2', '12.22');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tb_field_rsi
-- ----------------------------
ALTER TABLE "public"."tb_field_rsi" ADD PRIMARY KEY ("id");
