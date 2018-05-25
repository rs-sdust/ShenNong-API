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

Date: 2018-05-23 09:44:19
*/


-- ----------------------------
-- Table structure for tb_user
-- ----------------------------
DROP TABLE IF EXISTS "public"."tb_user";
CREATE TABLE "public"."tb_user" (
"id" int2 DEFAULT nextval('tb_user_id_seq'::regclass) NOT NULL,
"name" varchar(50) COLLATE "default" DEFAULT ''::character varying NOT NULL,
"mobile" varchar(11) COLLATE "default" DEFAULT ''::character varying NOT NULL,
"password" varchar(100) COLLATE "default" DEFAULT ''::character varying,
"im_pwd" varchar(100) COLLATE "default" DEFAULT ''::character varying NOT NULL,
"role" int4 DEFAULT '-1'::integer NOT NULL,
"farm" int4 DEFAULT '-1'::integer NOT NULL,
"icon" varchar(100) COLLATE "default" DEFAULT ''::character varying
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."tb_user" IS '用户信息表';
COMMENT ON COLUMN "public"."tb_user"."name" IS '昵称，注册后默认值是手机号';
COMMENT ON COLUMN "public"."tb_user"."password" IS '前期不设置密码';
COMMENT ON COLUMN "public"."tb_user"."im_pwd" IS '登录IM服务的密码（手机号加密）';
COMMENT ON COLUMN "public"."tb_user"."role" IS 'FK_角色ID';
COMMENT ON COLUMN "public"."tb_user"."farm" IS 'FK_农场编号';
COMMENT ON COLUMN "public"."tb_user"."icon" IS '用户头像连接';

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Uniques structure for table tb_user
-- ----------------------------
ALTER TABLE "public"."tb_user" ADD UNIQUE ("mobile");

-- ----------------------------
-- Primary Key structure for table tb_user
-- ----------------------------
ALTER TABLE "public"."tb_user" ADD PRIMARY KEY ("id");
