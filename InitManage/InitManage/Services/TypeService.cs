using InitManage.Commons;
using InitManage.Helpers.Interfaces;
using InitManage.Models.DTOs;
using InitManage.Models.Interfaces;
using InitManage.Services.Interfaces;
using Simple.Http;

namespace InitManage.Services;

public class TypeService : ITypeService
{
	private readonly IHttpService _httpService;
	private readonly IPreferenceHelper _preferenceHelper;

    public TypeService(IHttpService httpService, IPreferenceHelper preferenceHelper)
	{
		_httpService = httpService;
		_preferenceHelper = preferenceHelper;
    }

	public async Task<IEnumerable<ITypeEntity>> GetTypesAsync()
	{
		var response = await _httpService.SendRequestAsync<IEnumerable<TypeDTO>>($"{_preferenceHelper.ApiBaseAddress}{Constants.TypeEndPoint}", HttpMethod.Get);
		return response?.Result;
	}
}
