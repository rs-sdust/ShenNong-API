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

Date: 2018-05-15 14:22:33
*/


-- ----------------------------
-- Table structure for tb_farm
-- ----------------------------
DROP TABLE IF EXISTS "public"."tb_farm";
CREATE TABLE "public"."tb_farm" (
"id" int2 DEFAULT nextval('tb_farm_id_seq'::regclass) NOT NULL,
"name" varchar(100) COLLATE "default" DEFAULT ''::character varying NOT NULL,
"address" "public"."geometry" NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."tb_farm" IS '农场信息表';
COMMENT ON COLUMN "public"."tb_farm"."address" IS 'json格式存储的位置信息';

-- ----------------------------
-- Records of tb_farm
-- ----------------------------

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------
