use `ph-rbac`;
UPDATE sys_organize_relation SET STATE = -1 WHERE ORG_CODE NOT LIKE '1@-1@%' AND ORG_CODE <> '1@';


use `ph-rbac`;
select * from sys_organize_relation WHERE ORG_CODE NOT LIKE '1@-1@%' AND ORG_CODE <> '1@';

SELECT a.USER_ID, a.LOGIN_NAME, b.ORG_CODE, b.ORG_NAME FROM `dev-rbac`.sys_user_security a left join sys_organize_relation b on a.ORG_ID = b.ORG_ID WHERE a.LOGIN_NAME
in (
'C680352',
'C680377',
'C680680',
'C680528'
);


SELECT a.USER_ID, a.LOGIN_NAME, b.ORG_CODE, b.ORG_NAME FROM `dev-rbac`.sys_user_security a left join sys_organize_relation b on a.ORG_ID = b.ORG_ID WHERE a.LOGIN_NAME
in (
'C680352',
'C680377',
'C680680',
'C680528'
);


SELECT a.USER_ID, a.LOGIN_NAME, b.ORG_CODE, b.ORG_NAME FROM `dev-rbac`.sys_user_security a left join sys_organize_relation b on a.ORG_ID = b.ORG_ID WHERE a.USER_ID = 'ce7cd17d-3574-48d9-8690-c2c6d304765b' or b.ORG_Id = '350a93ff-7a86-421c-a06e-dc1fb7106d29';


 use `dev-rbac`;
SELECT a.USER_ID, a.LOGIN_NAME, b.ORG_CODE, b.ORG_NAME FROM `dev-rbac`.sys_user_security a left join sys_organize_relation b on a.ORG_ID = b.ORG_ID WHERE b.ORG_CODE NOT LIKE '1@%';

update uat-config.config_contract set CONTRACT_STATE = -1 WHERE CONTRACT_ID IN
(
'46945bca-52c7-4635-9e6e-bbba698c001d',
'15e1fe83-c01d-42f7-8289-4f7410341a8e',
'46454303-ba22-43d3-bffa-e7338f3d6f8b',
'5b2eaf56-df8b-4a23-afff-4a760161a032',
'60dfa5d1-4bd3-479b-9156-e93429810380',
'776e676f-cecc-4614-b24c-b5eb93271e03',
'1006024c-d926-4ccf-b6f7-7b03ad6e712e',
'4970e551-31d3-48fb-8187-881b777393f2',
'5e9da6ba-6f96-49c3-905d-b6c7070b90d6',
'0274bf8b-031a-4a50-91b6-40f5d2eb8b6c',
'02c64300-c1e8-4101-9562-c3005fe52c09',
'04760dbf-8a5f-40a5-bad8-20a2d0d26227',
'0a85544b-f845-4782-b357-ff3b92fd2bba',
'1539917a-b49c-4448-b5d2-efc3c7a0b630',
'37be2bc1-5ebf-4b64-8869-946a67b38677',
'406ee3a2-8bda-4f6a-a8d0-48b34e09ae3b',
'45797a47-0b49-407b-8b1f-b25c315d8306',
'4e919983-1783-4c98-8191-09d79135dd78',
'576b7e49-d6a4-4c27-bfca-392865c25046',
'60264dda-6c51-4cc3-8a45-22237c5bac57',
'6f9e101f-5b22-4854-a322-95b72a7e54e4',
'80dc0ca7-e1f6-4ec2-81d4-65378dde4b0e',
'862f0d0c-a37b-45c2-824a-e8bf0a97361c',
'865fb2cf-67af-46b7-8441-0938ad6a88f8',
'289686db-a56d-41d2-a9df-7ec5e5501624',
'3c8a1975-0587-48ed-a134-04562b7f3722',
'42f4d8d3-fe67-4069-baaa-3d0116a3d385',
'60e76348-4ee4-423d-b066-4191aa2cfd20',
'75dd679e-1736-4a91-98fd-9705e9734109',
'2648bf25-34b5-40f5-abb3-4a89a2756ebc',
'3cf963dd-7347-4486-90c4-d0998a5d44b2',
'496d2e9f-730f-46ea-9416-0d4d93f621fc',
'533a31bd-a76a-47be-a399-ea8079bba415',
'559b42b5-88f0-4dfd-836f-003b95e818d6',
'82d220d9-8e8b-4220-86ef-f51a8b6ec39a',
'06634bc0-fc7d-4ef0-a004-51c60cbc4f3a',
'0f952328-27b2-4693-b741-246394698cf0',
'32b306e0-f0e2-4ab4-b005-1024273a9acb',
'40840979-ff82-4e59-9c3f-32944680ed76',
'4b3eec46-0a81-4ca5-942f-e922d7e6f8ca',
'5ebf79d9-345c-44fc-a7dd-de711d9e15e4',
'69e73c27-7cd8-48c6-aee3-1628db45f366',
'7031bc7f-3418-42c8-bdfa-415c2d01cab5',
'7597e6f7-f3fd-4ee4-9058-935f63e3dce6',
'79c51943-984e-4a31-814f-0f8704201d69'
);

update config_contract set CONTRACT_STATE = -1 WHERE CONTRACT_ID IN
(
'80fe9ebb-1c4d-4182-9628-ba972a4ebc36',
'1e543c45-0083-46c3-89f7-c427d62aecef',
'2e9a54e6-7e04-4fce-8f63-149bd747adcd',
'05c130b5-5fa7-4a3f-8658-298d7283b48e'
);


update config_contract set CONTRACT_STATE = -1 WHERE CONTRACT_ID IN
(
'686fdba6-c739-4c51-a9de-d4e11db63035',
'80fe9ebb-1c4d-4182-9628-ba972a4ebc36'
);


DELETE FROM config_commodity_trade WHERE DEPOSIT_CURRENCY NOT IN (61, 62, 64, 65);
