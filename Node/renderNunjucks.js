var nunjucks = require('nunjucks');

module.exports = function (callback, template, data) {
	// callback(null,  template);
	var result = nunjucks.renderString(template, data);
	callback(null, result);
};