# 使用一个基础镜像
FROM python:3.8

# 设置工作目录
WORKDIR /automation

# 安装 Xvfb 和其他 X 相关工具
RUN apt-get update && apt-get install -y \
    xvfb \
    x11vnc \
    && rm -rf /var/lib/apt/lists/*

# 设置环境变量，模拟虚拟显示
ENV DISPLAY=:99

# 创建 .Xauthority 文件
RUN touch /root/.Xauthority

# 复制当前目录下的所有文件到工作目录
COPY . .

# 安装应用程序的依赖
RUN pip install -r requirements.txt
