








#!/bin/bash

machines=(
    "JGX-trade" "47.243.130.226" "/home/version3.0/pro_routine/c++/riskctrl_profit/riskControl.bin"
    "JGX-SL" "8.218.68.234" "/home/version3.0/pro_routine_sl/c++/riskctrl_profit/riskControl.bin"
    "TS-trade" "8.210.76.233" "/home/version3.0/pro_routine/c++/riskctrl_profit/riskControl.bin"
    "TS-SL" "47.242.153.199" "/home/version3.0/pro_routine_sl/c++/riskctrl_profit/riskControl.bin"
)

base_machine="10.10.100.129"
base_path="/home/version3.0/pro_routine/c++/riskctrl_profit/riskControl.bin"

base_size=$(ssh "root@$base_machine" "stat -c%s '$base_path'")

results=()

for ((i = 0; i < ${#machines[@]}; i+=3)); do
    machine="${machines[i]}"
    ip="${machines[i+1]}"
    path="${machines[i+2]}"

    size=$(ssh "otpitadmin@$ip" "stat -c%s '$path'")

    if [ "$base_size" != "$size" ]; then
        result="與UAT $base_size 文件不一樣的是：$machine，文件大小：$size"
    else
        result="與UAT $base_size 文件一樣的是：$machine，文件大小：$size"
    fi

    results+=("$result")
done

for result in "${results[@]}"; do
    echo "$result"
done

