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

Date: 2018-06-08 09:28:13
*/


-- ----------------------------
-- Table structure for tb_feedback
-- ----------------------------
DROP TABLE IF EXISTS "public"."tb_feedback";
CREATE TABLE "public"."tb_feedback" (
"id" int2 DEFAULT nextval('tb_feedback_id_seq'::regclass) NOT NULL,
"userid" int4 DEFAULT '-1'::integer NOT NULL,
"suggestion" varchar(255) COLLATE "default" DEFAULT ''::character varying NOT NULL,
"url" varchar(255) COLLATE "default" DEFAULT ''::character varying NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."tb_feedback" IS '意见反馈表';
COMMENT ON COLUMN "public"."tb_feedback"."userid" IS '用户id';
COMMENT ON COLUMN "public"."tb_feedback"."suggestion" IS '意见';
COMMENT ON COLUMN "public"."tb_feedback"."url" IS '图片链接';

-- ----------------------------
-- Records of tb_feedback
-- ----------------------------
INSERT INTO "public"."tb_feedback" VALUES ('1', '2', 'sfsaf', 'defref');
INSERT INTO "public"."tb_feedback" VALUES ('2', '1', '添加地块时，有问题！', 'Http://shgdka/shd');
INSERT INTO "public"."tb_feedback" VALUES ('3', '1', '添加地块时，有问题！', 'Http://shgdka/shd');
INSERT INTO "public"."tb_feedback" VALUES ('4', '1', '添加地块时，有问题！', 'Http://shgdka/shd');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tb_feedback
-- ----------------------------
ALTER TABLE "public"."tb_feedback" ADD PRIMARY KEY ("id");
