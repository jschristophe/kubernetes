: <<'END_COMMENT'

sudo apt-get update


sudo apt-get install -y \
  apt-transport-https \
  ca-certificates \
  curl \
  software-properties-common

curl -s \
 https://packages.cloud.google.com/apt/doc/apt-key.gpg | sudo apt-key add -

curl -fsSL \
 https://download.docker.com/linux/ubuntu/gpg | sudo apt-key add -

echo "deb https://apt.kubernetes.io/ kubernetes-xenial main" > /etc/apt/sources.list.d/kubernetes.list

sudo apt-get update

sudo apt-get install -y kubelet kubeadm kubectl

sudo kubeadm init


#############CONFIG####################

mkdir -p $HOME/.kube
sudo cp -i /etc/kubernetes/admin.conf $HOME/.kube/config
sudo chown $(id -u):$(id -g) $HOME/.kube/config


##### CREATE NETWORK #####
sudo kubectl apply -n kube-system -f  "https://cloud.weave.works/k8s/net?k8s-version=$(sudo kubectl version | base64 | tr -d '\n')"

END_COMMENT

######HELM

##sudo snap install helm --classic
curl https://raw.githubusercontent.com/helm/helm/master/scripts/get | bash

kubectl create serviceaccount tiller --namespace kube-system

kubectl create clusterrolebinding tiller-cluster-rule \
 clusterrole=cluster-admin \
 serviceaccount=kube-system:tiller

helm init --history-max 200 --service-account=tiller

