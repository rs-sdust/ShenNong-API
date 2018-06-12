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

Date: 2018-06-08 09:28:44
*/


-- ----------------------------
-- Table structure for tb_news
-- ----------------------------
DROP TABLE IF EXISTS "public"."tb_news";
CREATE TABLE "public"."tb_news" (
"id" int2 DEFAULT nextval('tb_news_id_seq'::regclass) NOT NULL,
"news_type" int4 DEFAULT '-1'::integer NOT NULL,
"summary" varchar(255) COLLATE "default" NOT NULL,
"news_date" date NOT NULL,
"url" varchar(255) COLLATE "default" NOT NULL,
"thumb" varchar(255) COLLATE "default" NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."tb_news" IS '新闻资讯表';
COMMENT ON COLUMN "public"."tb_news"."news_type" IS '资讯类型';
COMMENT ON COLUMN "public"."tb_news"."summary" IS '资讯简述';
COMMENT ON COLUMN "public"."tb_news"."news_date" IS '资讯日期';
COMMENT ON COLUMN "public"."tb_news"."url" IS '资讯链接';
COMMENT ON COLUMN "public"."tb_news"."thumb" IS '缩略图';

-- ----------------------------
-- Records of tb_news
-- ----------------------------
INSERT INTO "public"."tb_news" VALUES ('2', '1', '测试新闻1', '2018-06-13', 'http://www.xinhuanet.com/photo/2018-05/30/c_1122914159.htm', 'http://www.xinhuanet.com/photo/2018-05/30/1122914159_15276870735651n.jpg');
INSERT INTO "public"."tb_news" VALUES ('3', '1', '测试新闻2', '2018-06-05', 'http://www.xinhuanet.com/photo/2018-05/30/c_1122914159.htm', 'http://www.xinhuanet.com/photo/2018-05/30/1122914159_15276870735651n.jpg');
INSERT INTO "public"."tb_news" VALUES ('4', '2', '新闻', '2018-06-03', 'http://www.moa.gov.cn/xw/shipin/201805/t20180524_6143052.htm', 'http://www.xinhuanet.com/photo/2018-05/30/1122914159_15276870735651n.jpg');
INSERT INTO "public"."tb_news" VALUES ('5', '3', 'xinwn', '2018-06-05', 'http://www.moa.gov.cn/xw/shipin/201805/t20180524_6143052.htm', 'http://www.xinhuanet.com/photo/2018-05/30/1122914159_15276870735651n.jpg');
INSERT INTO "public"."tb_news" VALUES ('6', '1', '你玩', '2018-06-06', 'https://3w.huanqiu.com/a/a-XDI128FA78DACE07D2C4AC?agt=8', 'https://t1.huanqiu.cn/a778d16d02c25219dc53f291faf48ccc.jpg');
INSERT INTO "public"."tb_news" VALUES ('7', '1', 'fkmal', '2018-06-06', 'https://t1.huanqiu.cn/a778d16d02c25219dc53f291faf48ccc.jpg', 'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1528874047&di=22581fd8f90f36e57bbc47b1b5b023a5&imgtype=jpg&er=1&src=http%3A%2F%2Fimg5q.duitang.com%2Fuploads%2Fitem%2F201505%2F02%2F20150502182518_dV8ez.jpeg');
INSERT INTO "public"."tb_news" VALUES ('8', '2', '123', '2018-06-06', 'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1528874047&di=22581fd8f90f36e57bbc47b1b5b023a5&imgtype=jpg&er=1&src=http%3A%2F%2Fimg5q.duitang.com%2Fuploads%2Fitem%2F201505%2F02%2F20150502182518_dV8ez.jpeg', 'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1528874047&di=22581fd8f90f36e57bbc47b1b5b023a5&imgtype=jpg&er=1&src=http%3A%2F%2Fimg5q.duitang.com%2Fuploads%2Fitem%2F201505%2F02%2F20150502182518_dV8ez.jpeg');
INSERT INTO "public"."tb_news" VALUES ('9', '2', '123', '2018-06-06', 'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1528874137&di=b385b257de3c8f1df007e6a6f5897294&imgtype=jpg&er=1&src=http%3A%2F%2Fimg4q.duitang.com%2Fuploads%2Fitem%2F201409%2F13%2F20140913125843_Zc3yC.thumb.700_0.png', 'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1528874137&di=b385b257de3c8f1df007e6a6f5897294&imgtype=jpg&er=1&src=http%3A%2F%2Fimg4q.duitang.com%2Fuploads%2Fitem%2F201409%2F13%2F20140913125843_Zc3yC.thumb.700_0.png');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tb_news
-- ----------------------------
ALTER TABLE "public"."tb_news" ADD PRIMARY KEY ("id");
