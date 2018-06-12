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

Date: 2018-06-08 09:26:47
*/


-- ----------------------------
-- Table structure for dic_news_type
-- ----------------------------
DROP TABLE IF EXISTS "public"."dic_news_type";
CREATE TABLE "public"."dic_news_type" (
"id" int2 DEFAULT nextval('dic_news_type_id_seq'::regclass) NOT NULL,
"news_type" varchar(255) COLLATE "default" DEFAULT ''::character varying NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."dic_news_type" IS '资讯类型表';
COMMENT ON COLUMN "public"."dic_news_type"."news_type" IS '资讯类型';

-- ----------------------------
-- Records of dic_news_type
-- ----------------------------
INSERT INTO "public"."dic_news_type" VALUES ('1', '头条新闻');
INSERT INTO "public"."dic_news_type" VALUES ('2', '推荐新闻');
INSERT INTO "public"."dic_news_type" VALUES ('3', '全部新闻');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table dic_news_type
-- ----------------------------
ALTER TABLE "public"."dic_news_type" ADD PRIMARY KEY ("id");
