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

Date: 2018-05-16 14:44:31
*/


-- ----------------------------
-- Table structure for tb_field_live
-- ----------------------------
DROP TABLE IF EXISTS "public"."tb_field_live";
CREATE TABLE "public"."tb_field_live" (
"id" int2 DEFAULT nextval('tb_filed_live_id_seq'::regclass) NOT NULL,
"field" int4 DEFAULT '-1'::integer NOT NULL,
"growth" int4 DEFAULT '-1'::integer NOT NULL,
"moisture" int4 DEFAULT '-1'::integer NOT NULL,
"disease" varchar(50) COLLATE "default" DEFAULT ''::character varying,
"pest" varchar(50) COLLATE "default" DEFAULT ''::character varying,
"collector" int4 DEFAULT '-1'::integer NOT NULL,
"collect_date" date NOT NULL,
"gps" "public"."geometry" NOT NULL,
"picture" varchar COLLATE "default" NOT NULL
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "public"."tb_field_live" IS '地块实况信息表';
COMMENT ON COLUMN "public"."tb_field_live"."field" IS 'FK_地块编号';
COMMENT ON COLUMN "public"."tb_field_live"."growth" IS 'FK_长势等级';
COMMENT ON COLUMN "public"."tb_field_live"."moisture" IS 'FK_土壤湿度等级';
COMMENT ON COLUMN "public"."tb_field_live"."disease" IS '病害类型';
COMMENT ON COLUMN "public"."tb_field_live"."pest" IS '虫害类型';
COMMENT ON COLUMN "public"."tb_field_live"."collector" IS '采集人';
COMMENT ON COLUMN "public"."tb_field_live"."collect_date" IS '采集时间';
COMMENT ON COLUMN "public"."tb_field_live"."gps" IS '地理位置';
COMMENT ON COLUMN "public"."tb_field_live"."picture" IS '图片连接';

-- ----------------------------
-- Records of tb_field_live
-- ----------------------------
INSERT INTO "public"."tb_field_live" VALUES ('1', '1', '2', '3', '无疾病', '有虫害', '1', '2018-05-16', '01060000000100000001030000000100000062000000BC1493AEEE4D5D40ACD50EEEEA2F444010660F5E82585D40B873183BFD1C4440EEC122C1FF4C5D402B4B7496590A4440710C3FDA8D4B5D403A95B7239C0A4440B826982F844B5D4024E47396750944401076BAFB554B5D40AC6F54FF04094440E34DE4DA344B5D40E0F66AD8D308444067286EAF214B5D4012FA5011C3084440BEA98F5A7D485D40440BC443FC07444090D2A2C79A3D5D40ECE74D452A064440B0545051F5395D405241AB2496064440BA75782100365D40FBAD54BFEE064440D7639668C9345D40462E287B67064440FA1F1B4696315D4082388D5C53044440EA2FF13D3F315D40D0409E5DBE034440E64F9A5434315D40E727C9CB7E034440F3B72E8331315D40F1BD76D860034440BEFE994B8E305D400A68DD9717FC43406632532FBF315D40C643B7EF35F24340B0F8FBCF7E385D405633A48AE2D94340C013A9ECD8385D40B6D381ACA7D843405E1D88AB91395D404A7D4D670BD84340EA8C6550A6335D40C26FFA22BDCE4340BF08E0879F2C5D406C39369353CB4340A6366AB83A1C5D40CE340C1F11BD434086367AE1400F5D402A51A1BAB9C24340963C5B30460A5D40A4AAA85297CA434073AAEA8FF7F95C40B7E2993FC2C84340A399DFB721F75C4009513CD6A8C54340D01A3CE658F45C40CED88764E4C04340FF950BEBFFF25C40D6FA88F013C1434052969C0BEAEF5C407BAE6186C6C14340F3909E99B5EF5C40F5972268E8C143400AA735D5CCE35C4013638D0BEBCC4340A0C6EAE731E05C40BEAC342905D34340EE13CD2406E05C403E36F2E862D3434082781A5987DB5C40FC0994A5BAE04340319022302BDB5C4099C99D2AC3E24340EF1DA9E9B3DA5C40E4422A342CFA43407011D785ADDA5C40E25EC6A50EFC4340EEDFB635B0DA5C409CBCF5F21FFC4340E8B1A1C6BDDA5C408439ECBE63FC43400449533508DB5C40549F11FDBEFD434088E45926FCDB5C406E93FC885F01444032103B2828DC5C405A6CC078EA014440688BAC4EB1DC5C40881CCD9195034440226156CAC0DC5C4035D2C43BC003444018450B7A36E15C40CEAE264F59094440E885ED87F5E55C40822B57E7340F4440CA102D0A9FEB5C40D63E4A7D3D11444000C5F37FF2F55C40AE007E1E87174440E235A7E600FD5C40604F68EA59224440208915DD96FA5C403B5A8C29742A4440199D3426A7F85C40D429F5BD6A2D4440D6CEB0EB33F85C403AAEACC58B2D4440E87968060EF75C4036203254E12D44401ADE675AB7F65C407E4B2DEC4D2E44402A5F969718F55C405EAFE94141314440323E087827F15C40245514AFB2384440290772A4C1EE5C40CEB94DB85740444068F597A8C1EE5C40EF49A5476F404440E6982CEE3FEF5C40EE01207F4D42444042EC824A07F05C40D4997B48F844444093A1D2116BF15C405B7ED4B7B04644402004D2D5C8F15C40E3D7033E2347444000AB9B0C00F25C40C1D1F46A64474440FA68EDFAE8F95C409DD5F272F44E4440B2EA2124EFF95C406C7F677BF44E4440A092248997FA5C40EAC41EDAC74E4440535772739CFC5C40B0E376D7F54D44407E7A2CB039FD5C40F4C5504EB44D44407A2E50484EFD5C4042AE4676A54D4440A8AE6C2173FD5C408E7F8F69894D4440A291357B84FD5C40B4BC5642774D44405A1B237991FD5C4048A505C05C4D444038FEFC45D0FD5C40B91C66D7D94C4440A004FC3BF7FD5C401A6C9560714C44401B5546C30CFE5C40DA5B751DAA4B444016E59A4E07FE5C402BF1C5868A4B4440DB2D82990BFE5C4044A556075C4B4440D0A09F7E17FE5C40BC588AE42B4B4440675C37C66AFE5C405A0AAE4A064A4440589DCC478BFE5C406686813FE0494440181B3758FFFE5C40DC59ABE5EA4944402AA9015A65FF5C409E232BBF0C4A4440203E6F7E51045D40D299ED0A7D4E4440EFCD26C2311A5D40B6EB504D49624440D93636EFE31A5D40862F79942E694440E0FE1DD060175D40DCC95E5E9C784440AEA2B843E0265D40978BAFE5B18444402C28C2A9D62B5D4026EBB548F68044403673C1E6AA2F5D4074943A70EA714440F8C95BF25B3A5D406555C96FED624440AF50A6FCA4465D40A22FC970585A44405B509F847C555D40F023E414015644401A08315377575D4028F251D7BE4844407221BACDFF4E5D40C27752CE333A4440BC1493AEEE4D5D40ACD50EEEEA2F4440', 'sde');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tb_field_live
-- ----------------------------
ALTER TABLE "public"."tb_field_live" ADD PRIMARY KEY ("id");