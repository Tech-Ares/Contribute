#!/bin/bash
#不同的專案有不同的路徑
array[0]='project1'
array[1]='project2'
array[2]='com/project3'
array[3]='com/phase/project4'
array[4]='project5'
array[5]='com/stor/sproject6'
#專案的主幹目錄是相同的
RELEASE="/opt/devapps/nexus/sonatype-work/nexus/storage/release/"
for path in ${array[@]};
do
#拼接檔案路徑
releasepath=${RELEASE}${path}
cd $releasepath
#判斷是否存在該目錄
if [ $? -eq 0 ];
then
echo $releasepath
echo "Contains file:"
#輸出所有的內容
echo *
num=`ls -l | grep '^d' | wc -l`;
#判斷資料夾的數量是否超過5個（我只想保留最新的5個資料夾）
if [$num -gt 5 ];
then
#計算超過5個多少
num=`expr $num - 5`
clean=`ls -tr | head -$num | xargs`
echo "will delete file:"
echo ${clean}
#-n1 每次處理1個檔案
ls -tr | head -$num | xargs -i -n1 rm -rf {}
fi
fi
done