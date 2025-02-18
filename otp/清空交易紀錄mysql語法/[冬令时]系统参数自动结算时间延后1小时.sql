update TSystemParameter set FItemValueStr = concat('0',left(FItemValueStr,2)+1,right(FItemValueStr,LENGTH(FItemValueStr)-2))
where FItemNo= 'SETTLETIME' and left(FItemValueStr,2)='05';