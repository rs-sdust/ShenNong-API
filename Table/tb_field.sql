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

Date: 2018-05-15 14:22:40
*/


-- ----------------------------
-- Table structure for tb_field
-- ----------------------------
DROP TABLE IF EXISTS "public"."tb_field";
CREATE TABLE "public"."tb_field" (
"id" int2 DEFAULT nextval('tb_field_id_seq'::regclass) NOT NULL,
"farm" int4 DEFAULT '-1'::integer NOT NULL,
"name" varchar(100) COLLATE "default" DEFAULT ''::character varying NOT NULL,
"geom" "public"."geometry",
"area" float4 DEFAULT 0 NOT NULL,
"createdate" date NOT NULL,
"currentcrop" int4 DEFAULT '-1'::integer,
"sowdate" date,
"phenophase" int4 DEFAULT '-1'::integer NOT NULL,
"thumb" varchar(255) COLLATE "default" DEFAULT ''::character varying
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."tb_field" IS '地块信息表';
COMMENT ON COLUMN "public"."tb_field"."name" IS '地块名称';
COMMENT ON COLUMN "public"."tb_field"."area" IS '以亩为单位';
COMMENT ON COLUMN "public"."tb_field"."createdate" IS '创建时间';
COMMENT ON COLUMN "public"."tb_field"."currentcrop" IS 'FK_作物类型编号';
COMMENT ON COLUMN "public"."tb_field"."sowdate" IS '播种时间';
COMMENT ON COLUMN "public"."tb_field"."phenophase" IS '以天为单位定时更新';
COMMENT ON COLUMN "public"."tb_field"."thumb" IS '地块缩略图';

-- ----------------------------
-- Records of tb_field
-- ----------------------------
INSERT INTO "public"."tb_field" VALUES ('0', '1', '西地', null, '123.33', '2018-05-02', '1', '2018-05-15', '25', null);
INSERT INTO "public"."tb_field" VALUES ('1', '1', '东地', null, '25.21', '2018-05-23', '1', '2018-03-26', '65', null);
INSERT INTO "public"."tb_field" VALUES ('2', '2', '西地', null, '254.32', '2018-04-01', '1', '2018-05-01', '25', null);
INSERT INTO "public"."tb_field" VALUES ('3', '1', '南地', null, '25.22', '2018-05-11', '2', '2018-03-06', '21', null);
INSERT INTO "public"."tb_field" VALUES ('4', '1', '4fff', null, '34.2', '2018-05-15', '2', null, '21', null);
INSERT INTO "public"."tb_field" VALUES ('5', '-1', '', null, '254.22', '2018-05-15', '-1', null, '-1', '');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tb_field
-- ----------------------------
ALTER TABLE "public"."tb_field" ADD PRIMARY KEY ("id");
