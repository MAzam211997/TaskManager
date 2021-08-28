export const environment = {
  production: false,
  urlAddress: getEnvironment(),
};
function getEnvironment() {
  //var env;
  //env = window.location;

  //if (env.hostname === "localhost")
  //    env = 'http://localhost:51912/'; // Url for development server
  //else if (env.hostname === '192.168.10.106')
  //    env = 'http://192.168.10.106:81/'; //Url for local build server
  //else if (env.hostname === 'erp.razorconsultants.com')
  //    env = 'http://erp.razorconsultants.com/'; //Url for production server
  //else if (env.hostname === 'erpqa.razorconsultants.com')
  //    env = 'http://erpqa.razorconsultants.com/'; //Url for production server
  //else if (env.hostname === 'erpa.razorconsultants.com')
  //    env = 'http://erpa.razorconsultants.com/'; //Url for production server
  //else if (env.hostname === 'erph.razorconsultants.com')
  //    env = 'http://erph.razorconsultants.com/'; //Url for production server
  return '';
}
