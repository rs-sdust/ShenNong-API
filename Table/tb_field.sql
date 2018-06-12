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

Date: 2018-06-08 09:28:20
*/


-- ----------------------------
-- Table structure for tb_field
-- ----------------------------
DROP TABLE IF EXISTS "public"."tb_field";
CREATE TABLE "public"."tb_field" (
"id" int2 DEFAULT nextval('tb_field_id_seq'::regclass) NOT NULL,
"farm" int4 DEFAULT '-1'::integer NOT NULL,
"name" varchar(100) COLLATE "default" DEFAULT ''::character varying NOT NULL,
"geom" varchar COLLATE "default",
"area" float4 DEFAULT 0 NOT NULL,
"createdate" date NOT NULL,
"currentcrop" int4 DEFAULT '-1'::integer,
"sowdate" date,
"phenophase" int4 DEFAULT '-1'::integer NOT NULL,
"thumb" varchar(1024) COLLATE "default" DEFAULT ''::character varying
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
INSERT INTO "public"."tb_field" VALUES ('19', '12', '张晓智的测试地块1', '0103000000010000000500000047D2F3FB8DB6424094842ACB3E265D402229B4F558B64240947AC50233265D4058C535A636B642400CEFD7DE70265D402C737BD365B64240C0AB2DD87F265D4047D2F3FB8DB6424094842ACB3E265D40', '67403.1', '2018-06-05', '3', '2018-05-05', '25', 'http://img.taopic.com/uploads/allimg/140327/235088-14032GP44387.jpg');
INSERT INTO "public"."tb_field" VALUES ('21', '1', '南地3', '0103000000010000000500000047D2F3FB8DB6424094842ACB3E265D402229B4F558B64240947AC50233265D4058C535A636B642400CEFD7DE70265D402C737BD365B64240C0AB2DD87F265D4047D2F3FB8DB6424094842ACB3E265D40', '21', '2018-06-06', '2', '2018-03-15', '17', 'sdffaf');
INSERT INTO "public"."tb_field" VALUES ('22', '12', '张晓智的测试地块2', '010300000001000000050000008D771C12A6B6424084539C957D265D40BC8F737673B64240905C4FDD71265D4078B4A7C65CB64240A06598189F265D40CB5925EE8DB64240B452B311A8265D408D771C12A6B6424084539C957D265D40', '44521.3', '2018-06-06', '1', '0001-01-01', '-1', 'http://img.taopic.com/uploads/allimg/140327/235088-14032GP44387.jpg');
INSERT INTO "public"."tb_field" VALUES ('23', '12', '张晓智的测试地块3', '01030000000100000005000000FACBC1C59BB64240601264281F265D401A5C93C16CB6424040BF7C6C70265D4065E284DEA2B6424090AF567375265D40C542291AD4B6424064608D9B2A265D40FACBC1C59BB64240601264281F265D40', '86231.3', '2018-06-07', '4', '2018-04-07', '39', 'http://img.taopic.com/uploads/allimg/140327/235088-14032GP44387.jpg');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tb_field
-- ----------------------------
ALTER TABLE "public"."tb_field" ADD PRIMARY KEY ("id");
