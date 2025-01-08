create index transferlog_loginname_index
	on transferlog (loginname);

create index transferlog_org_index
	on transferlog (org);

create index transferlog_topOrg_index
	on transferlog (topOrg);

create index transferlog_currency_index
	on transferlog (currency);

create index transferlog_billno_index
	on transferlog (billno);