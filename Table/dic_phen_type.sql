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

Date: 2018-06-08 09:27:03
*/


-- ----------------------------
-- Table structure for dic_phen_type
-- ----------------------------
DROP TABLE IF EXISTS "public"."dic_phen_type";
CREATE TABLE "public"."dic_phen_type" (
"id" int2 DEFAULT nextval('dic_phen_type_id_seq'::regclass) NOT NULL,
"phen_type" varchar(255) COLLATE "default" DEFAULT ''::character varying NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."dic_phen_type" IS '物候类型表';
COMMENT ON COLUMN "public"."dic_phen_type"."phen_type" IS '物候类型';

-- ----------------------------
-- Records of dic_phen_type
-- ----------------------------
INSERT INTO "public"."dic_phen_type" VALUES ('1', '播种期');
INSERT INTO "public"."dic_phen_type" VALUES ('2', '出苗期');
INSERT INTO "public"."dic_phen_type" VALUES ('3', '分蘖期');
INSERT INTO "public"."dic_phen_type" VALUES ('4', '越冬期');
INSERT INTO "public"."dic_phen_type" VALUES ('5', '返青期');
INSERT INTO "public"."dic_phen_type" VALUES ('6', '拔节期');
INSERT INTO "public"."dic_phen_type" VALUES ('7', '孕穗期');
INSERT INTO "public"."dic_phen_type" VALUES ('8', '抽穗期');
INSERT INTO "public"."dic_phen_type" VALUES ('9', '开花期');
INSERT INTO "public"."dic_phen_type" VALUES ('10', '灌浆期');
INSERT INTO "public"."dic_phen_type" VALUES ('11', '完熟期');
INSERT INTO "public"."dic_phen_type" VALUES ('12', '已收割');
INSERT INTO "public"."dic_phen_type" VALUES ('13', '三叶期');
INSERT INTO "public"."dic_phen_type" VALUES ('14', '小喇叭口期');
INSERT INTO "public"."dic_phen_type" VALUES ('15', '大喇叭口期');
INSERT INTO "public"."dic_phen_type" VALUES ('16', '吐丝期');
INSERT INTO "public"."dic_phen_type" VALUES ('17', '抽雄期');
INSERT INTO "public"."dic_phen_type" VALUES ('18', '抽丝期');
INSERT INTO "public"."dic_phen_type" VALUES ('19', '子粒形成期');
INSERT INTO "public"."dic_phen_type" VALUES ('20', '乳熟期');
INSERT INTO "public"."dic_phen_type" VALUES ('21', '蜡熟期');
INSERT INTO "public"."dic_phen_type" VALUES ('22', '播种育苗期');
INSERT INTO "public"."dic_phen_type" VALUES ('23', '拔节期');
INSERT INTO "public"."dic_phen_type" VALUES ('24', '扬花期');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table dic_phen_type
-- ----------------------------
ALTER TABLE "public"."dic_phen_type" ADD PRIMARY KEY ("id");
