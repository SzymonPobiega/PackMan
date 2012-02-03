PackMan.exe -i d:$(PackageDir) -o $(OutDir)\Package-$(BuildVersion).zip --cn DeployerCert -a create
PackMan.exe -p ${package_store}\${package_file} --vcn DeployerCert -a unpack -d ${tmp_package_dir}