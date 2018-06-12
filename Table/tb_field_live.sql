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

Date: 2018-06-08 09:28:26
*/


-- ----------------------------
-- Table structure for tb_field_live
-- ----------------------------
DROP TABLE IF EXISTS "public"."tb_field_live";
CREATE TABLE "public"."tb_field_live" (
"id" int2 DEFAULT nextval('tb_field_live_id_seq'::regclass) NOT NULL,
"field" int4 DEFAULT '-1'::integer NOT NULL,
"growth" int4 DEFAULT '-1'::integer NOT NULL,
"moisture" int4 DEFAULT '-1'::integer NOT NULL,
"disease" int4 DEFAULT '-1'::integer NOT NULL,
"pest" int4 DEFAULT '-1'::integer NOT NULL,
"collector" int4 DEFAULT '-1'::integer NOT NULL,
"collect_date" date NOT NULL,
"gps" "public"."geometry" NOT NULL,
"picture" varchar(1024) COLLATE "default" NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."tb_field_live" IS '地块实况信息表';
COMMENT ON COLUMN "public"."tb_field_live"."field" IS 'FK_地块编号';
COMMENT ON COLUMN "public"."tb_field_live"."growth" IS 'FK_长势等级';
COMMENT ON COLUMN "public"."tb_field_live"."moisture" IS 'FK_土壤湿度等级';
COMMENT ON COLUMN "public"."tb_field_live"."disease" IS 'FK_病害类型';
COMMENT ON COLUMN "public"."tb_field_live"."pest" IS 'FK_虫害类型';
COMMENT ON COLUMN "public"."tb_field_live"."collector" IS '采集人';
COMMENT ON COLUMN "public"."tb_field_live"."collect_date" IS '采集时间';
COMMENT ON COLUMN "public"."tb_field_live"."gps" IS '地理位置';
COMMENT ON COLUMN "public"."tb_field_live"."picture" IS '图片连接';

-- ----------------------------
-- Records of tb_field_live
-- ----------------------------
INSERT INTO "public"."tb_field_live" VALUES ('5', '8', '3', '2', '2', '0', '19', '2018-06-01', '01010000005118CF42F6FA43403EB83F3F5F175D40', '[]');
INSERT INTO "public"."tb_field_live" VALUES ('6', '7', '2', '1', '5', '4', '19', '2018-06-03', '010100000007D706F9F3FA43401C6F02713F175D40', '[]');
INSERT INTO "public"."tb_field_live" VALUES ('7', '7', '1', '3', '4', '4', '19', '2018-06-03', '0101000000AB665C5EF5FA4340243F8A4260175D40', '[{"path":"~\/User_Img\/15111462494\/2018060317285465045.jpg"}]');
INSERT INTO "public"."tb_field_live" VALUES ('8', '7', '1', '3', '4', '4', '19', '2018-06-03', '0101000000F30C034BFAFA43408AF22A4A47175D40', '[{"path":"~\/User_Img\/15111462494\/2018060317313368325.jpg"}]');
INSERT INTO "public"."tb_field_live" VALUES ('9', '7', '1', '3', '4', '4', '19', '2018-06-03', '0101000000F30C034BFAFA43408AF22A4A47175D40', '[{"path":"~\/User_Img\/15111462494\/2018060317313368325.jpg"},{"path":"~\/User_Img\/15111462494\/201806031731344206.jpg"}]');
INSERT INTO "public"."tb_field_live" VALUES ('10', '7', '1', '3', '4', '4', '19', '2018-06-03', '0101000000232C134CF7FA4340F795382F66175D40', '[{"path":"~\/User_Img\/15111462494\/2018060317465299630.jpg"}]');
INSERT INTO "public"."tb_field_live" VALUES ('11', '3', '2', '1', '3', '2', '2', '2018-06-03', '01010000000BD706F9F3FA4340196F02713F175D40', 'sdas');
INSERT INTO "public"."tb_field_live" VALUES ('12', '3', '2', '1', '3', '2', '2', '2018-06-03', '01010000000BD706F9F3FA4340196F02713F175D40', 'sdas');
INSERT INTO "public"."tb_field_live" VALUES ('13', '1', '1', '1', '1', '1', '1', '2018-05-28', '01010000000BD706F9F3FA4340196F02713F175D40', 'sfaff');
INSERT INTO "public"."tb_field_live" VALUES ('14', '8', '3', '3', '5', '4', '19', '2018-06-04', '010100000040B783A305FB434018C007F971175D40', '[{"path":"~\/User_Img\/15111462494\/2018060411271733557.jpg"}]');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tb_field_live
-- ----------------------------
ALTER TABLE "public"."tb_field_live" ADD PRIMARY KEY ("id");
