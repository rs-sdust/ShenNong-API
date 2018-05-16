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

Date: 2018-05-15 14:23:20
*/


-- ----------------------------
-- Table structure for tb_user_task
-- ----------------------------
DROP TABLE IF EXISTS "public"."tb_user_task";
CREATE TABLE "public"."tb_user_task" (
"id" int2 DEFAULT nextval('tb_user_task_id_seq'::regclass) NOT NULL,
"creator" int4 DEFAULT '-1'::integer NOT NULL,
"farm" int4 DEFAULT '-1'::integer NOT NULL,
"examiner" int4 DEFAULT '-1'::integer NOT NULL,
"type" int4 DEFAULT '-1'::integer NOT NULL,
"description" varchar(255) COLLATE "default" DEFAULT ''::character varying NOT NULL,
"state" int4 DEFAULT '-1'::integer NOT NULL,
"agree" bool DEFAULT false NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."tb_user_task" IS '待处理通知表';
COMMENT ON COLUMN "public"."tb_user_task"."creator" IS 'FK_用户编号';
COMMENT ON COLUMN "public"."tb_user_task"."farm" IS 'FK_农场编号';
COMMENT ON COLUMN "public"."tb_user_task"."examiner" IS 'FK_用户编号';
COMMENT ON COLUMN "public"."tb_user_task"."type" IS '0，申请加入农场；
1，农事计划通知';
COMMENT ON COLUMN "public"."tb_user_task"."state" IS '处理状态';
COMMENT ON COLUMN "public"."tb_user_task"."agree" IS '是否同意';

-- ----------------------------
-- Records of tb_user_task
-- ----------------------------
INSERT INTO "public"."tb_user_task" VALUES ('0', '1', '1', '1', '0', '加入农场，请批准', '0', 'f');
INSERT INTO "public"."tb_user_task" VALUES ('1', '2', '1', '1', '0', '加入农场，请批准', '0', 'f');
INSERT INTO "public"."tb_user_task" VALUES ('2', '3', '1', '1', '0', '加入农场，请批准', '0', 'f');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tb_user_task
-- ----------------------------
ALTER TABLE "public"."tb_user_task" ADD PRIMARY KEY ("id");
