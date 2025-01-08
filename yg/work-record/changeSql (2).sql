alter table ygg_player modify balance double(16,2) default 0.00 null;

alter table ygglog modify amount double(16,2) default 0.00 null;

alter table ygglog modify beforeAmount double(16,2) default 0.00 null;

alter table ygglog modify afterAmount double(16,2) default 0.00 null;

alter table transferlog modify amount double(16,2) null;


