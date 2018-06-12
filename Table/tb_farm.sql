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

Date: 2018-06-08 09:28:04
*/


-- ----------------------------
-- Table structure for tb_farm
-- ----------------------------
DROP TABLE IF EXISTS "public"."tb_farm";
CREATE TABLE "public"."tb_farm" (
"id" int2 DEFAULT nextval('tb_farm_id_seq'::regclass) NOT NULL,
"name" varchar(100) COLLATE "default" DEFAULT ''::character varying NOT NULL,
"address" varchar(255) COLLATE "default" NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."tb_farm" IS '农场信息表';
COMMENT ON COLUMN "public"."tb_farm"."address" IS 'json格式存储的位置信息';

-- ----------------------------
-- Records of tb_farm
-- ----------------------------
INSERT INTO "public"."tb_farm" VALUES ('1', '御耕农场', '{ 山东, 德州, 陵城县}');
INSERT INTO "public"."tb_farm" VALUES ('2', '御耕2农场', '{ 山东, 德州, 陵城县}');
INSERT INTO "public"."tb_farm" VALUES ('3', '御耕1农场', '{ 山东, 德州, 陵城县}');
INSERT INTO "public"."tb_farm" VALUES ('4', '裕德农场', '{ 山东, 德州, 陵城县}');
INSERT INTO "public"."tb_farm" VALUES ('5', '裕德农场2', '{ 山东, 德州, 陵城县}');
INSERT INTO "public"."tb_farm" VALUES ('6', 'dwe', '{山东，菏泽，牡丹区}');
INSERT INTO "public"."tb_farm" VALUES ('7', 'ddeee', '{山东，菏泽，牡丹区}');
INSERT INTO "public"."tb_farm" VALUES ('11', '张晓智的农场', '{北京,北京,昌平}');
INSERT INTO "public"."tb_farm" VALUES ('12', '张晓智', '{山东,德州,陵县}');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------
