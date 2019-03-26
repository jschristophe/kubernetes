: <<'END_COMMENT'
END_COMMENT

################# INSTAL DOCKER #################
sudo su
apt-get update 
apt-get install -y docker.io

################# Install kubeadm, Kubelet And Kubectl #################
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




#############CONFIG####################

mkdir -p $HOME/.kube

sudo cp -i /etc/kubernetes/admin.conf $HOME/.kube/config

sudo chown $(id -u):$(id -g) $HOME/.kube/config

sudo cp /etc/kubernetes/admin.conf $HOME/

sudo chown $(id -u):$(id -g) $HOME/admin.conf

export KUBECONFIG=$HOME/admin.conf


##### CREATE NETWORK #####
#sudo kubectl apply -n kube-system -f  "https://cloud.weave.works/k8s/net?k8s-version=$(sudo kubectl version | base64 | tr -d '\n')"




