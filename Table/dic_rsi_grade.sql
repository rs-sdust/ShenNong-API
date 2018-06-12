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

Date: 2018-06-08 09:27:15
*/


-- ----------------------------
-- Table structure for dic_rsi_grade
-- ----------------------------
DROP TABLE IF EXISTS "public"."dic_rsi_grade";
CREATE TABLE "public"."dic_rsi_grade" (
"id" int2 DEFAULT nextval('dic_rsi_grade_id_seq'::regclass) NOT NULL,
"grade" int4 DEFAULT '-1'::integer NOT NULL,
"rsi_type" int4 DEFAULT '-1'::integer NOT NULL,
"name" varchar(10) COLLATE "default" DEFAULT ''::character varying NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."dic_rsi_grade" IS '产品等级字典表';

-- ----------------------------
-- Records of dic_rsi_grade
-- ----------------------------
INSERT INTO "public"."dic_rsi_grade" VALUES ('1', '1', '2', '十大首富');
INSERT INTO "public"."dic_rsi_grade" VALUES ('2', '2', '2', 'test222');
INSERT INTO "public"."dic_rsi_grade" VALUES ('3', '3', '2', 'test332');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table dic_rsi_grade
-- ----------------------------
ALTER TABLE "public"."dic_rsi_grade" ADD PRIMARY KEY ("id");
