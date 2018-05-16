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

Date: 2018-05-16 15:45:44
*/


-- ----------------------------
-- Table structure for dic_role
-- ----------------------------
DROP TABLE IF EXISTS "public"."dic_role";
CREATE TABLE "public"."dic_role" (
"id" int2 DEFAULT nextval('dic_role_id_seq'::regclass) NOT NULL,
"name" varchar(10) COLLATE "default" DEFAULT ''::character varying NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."dic_role" IS '角色字典表';
COMMENT ON COLUMN "public"."dic_role"."id" IS '角色编号';
COMMENT ON COLUMN "public"."dic_role"."name" IS '角色名称';

-- ----------------------------
-- Records of dic_role
-- ----------------------------
INSERT INTO "public"."dic_role" VALUES ('0', '农场主');
INSERT INTO "public"."dic_role" VALUES ('1', '管理员');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table dic_role
-- ----------------------------
ALTER TABLE "public"."dic_role" ADD PRIMARY KEY ("id");
