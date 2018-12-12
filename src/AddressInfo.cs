using System;

namespace Haack.Postal
{
	/// <summary>
	/// Address Info.
	/// </summary>
	public struct AddressInfo
	{
		string _streetAddress;
		string _city;
		State _state;
		string _postalCode;
		string _country;
 
		/// <summary>
		/// Gets the street address.
		/// </summary>
		/// <value></value>
		public string StreetAddress
		{
			get { return _streetAddress; }
		}
            
		/// <summary>
		/// Gets the city.
		/// </summary>
		/// <value></value>
		public string City
		{
			get { return _city; }
		}
 
		/// <summary>
		/// Gets the state.
		/// </summary>
		/// <value></value>
		public State State
		{
			get { return _state; }
		}
 
		/// <summary>
		/// Gets the postal code.
		/// </summary>
		/// <value></value>
		public string PostalCode
		{
			get { return _postalCode; }
		}
 
		/// <summary>
		/// Gets the country.
		/// </summary>
		/// <value></value>
		public string Country
		{
			get { return _country; }
		}
            
            
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="streetAddress"></param>
		/// <param name="city"></param>
		/// <param name="state"></param>
		/// <param name="postalCode"></param>
		/// <param name="country"></param>
		public AddressInfo(string streetAddress, string city, string state, string postalCode, string country)
		{
			_streetAddress = streetAddress;
			_city = city;
			_state = AddressInfo.ParseState(state);
			_postalCode = postalCode;
			_country = country;
		}
 
		#region State Information
		/// <summary>
		/// Gets the state given the state code.
		/// </summary>
		/// <param name="stateCode">State code.</param>
		/// <returns></returns>
		public static string GetState(StateCode stateCode)
		{
			return Convert(stateCode).ToString().Replace("_", " ");
		}
 
		/// <summary>
		/// Returns the two letter state code for the given State enum value.
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		public static string GetStateCode(State state)
		{
			return Convert(state).ToString();
		}
 
		/// <summary>
		/// Converts a State to a StateCode.
		/// </summary>
		/// <param name="value">A State enum value.</param>
		/// <returns></returns>
		public static StateCode Convert(State value)
		{
			return (StateCode)value;
		}
 
		/// <summary>
		/// Converts a StateCode to a State.
		/// </summary>
		/// <param name="value">A StateCode enum value.</param>
		/// <returns></returns>
		public static State Convert(StateCode value)
		{
			return (State)value;
		}
 
		/// <summary>
		/// Returns the State enum value for the state.
		/// </summary>
		/// <param name="state">Name of the state.</param>
		/// <returns></returns>
		public static State ParseState(string state)
		{
			return (State)Enum.Parse(typeof(State), state.Replace(" ", ""), true);
		}
 
		/// <summary>
		/// Returns the StateCode enum value for the state.
		/// </summary>
		/// <param name="stateCode">Two letter state code</param>
		/// <returns></returns>
		public static StateCode ParseStateCode(string stateCode)
		{
			return (StateCode)Enum.Parse(typeof(StateCode), stateCode, true);
		}
		#endregion
	}
 
	/// <summary>
	/// Simple enum for all the states.
	/// </summary>
	#region enum State
	public enum State
	{
		/// <summary>AL</summary>
		Alabama,
		/// <summary>AK (Home Sweet Home)</summary>
		Alaska,
		/// <summary>AS</summary>
		American_Samoa,
		/// <summary>AZ</summary>
		Arizona,
		/// <summary>AR</summary>
		Arkansas,
		/// <summary>CA (Home of SkillJam Technologies).</summary>
		California,
		/// <summary>CO</summary>
		Colorado,
		/// <summary>CN</summary>
		Connecticut,
		/// <summary>DE</summary>
		Delaware,
		/// <summary>DC</summary>
		District_Of_Columbia,
		/// <summary>FM</summary>
		Federated_States_Of_Micronesia,
		/// <summary>FL</summary>
		Florida,
		/// <summary>GA</summary>
		Georgia,
		/// <summary>GU</summary>
		Guam,
		/// <summary>HI</summary>
		Hawaii,
		/// <summary>ID</summary>
		Idaho,
		/// <summary>IL</summary>
		Illinois,
		/// <summary>IN</summary>
		Indiana,
		/// <summary>IA</summary>
		Iowa,
		/// <summary>KS</summary>
		Kansas,
		/// <summary>KY</summary>
		Kentucky,
		/// <summary>LA</summary>
		Louisiana,
		/// <summary>ME</summary>
		Maine,
		/// <summary>MH</summary>
		Marshall_Islands,
		/// <summary>MD</summary>
		Maryland,
		/// <summary>MA</summary>
		Massachusetts,
		/// <summary>MI</summary>
		Michigan,
		/// <summary>MN</summary>
		Minnesota,
		/// <summary>MS</summary>
		Mississippi,
		/// <summary>MO</summary>
		Missouri,
		/// <summary>MT</summary>
		Montana,
		/// <summary>NE</summary>
		Nebraska,
		/// <summary>NV</summary>
		Nevada,
		/// <summary>NH</summary>
		New_Hampshire,
		/// <summary>NJ</summary>
		New_Jersey,
		/// <summary>NM</summary>
		New_Mexico,
		/// <summary>NY</summary>
		New_York,
		/// <summary>NC</summary>
		North_Carolina,
		/// <summary>ND</summary>
		North_Dakota,
		/// <summary>MP</summary>
		Northern_Mariana_Islands,
		/// <summary>OH</summary>
		Ohio,
		/// <summary>OK</summary>
		Oklahoma,
		/// <summary>OR</summary>
		Oregon,
		/// <summary>PW</summary>
		Palau,
		/// <summary>PA</summary>
		Pennsylvania,
		/// <summary>PR</summary>
		Puerto_Rico,
		/// <summary>RI</summary>
		Rhode_Island,
		/// <summary>SC</summary>
		South_Carolina,
		/// <summary>SD</summary>
		South_Dakota,
		/// <summary>TN</summary>
		Tennessee,
		/// <summary>TX</summary>
		Texas,
		/// <summary>UT</summary>
		Utah,
		/// <summary>VT</summary>
		Vermont,
		/// <summary>VI</summary>
		Virgin_Islands,
		/// <summary>VA</summary>
		Virginia,
		/// <summary>WA</summary>
		Washington,
		/// <summary>WV</summary>
		West_Virginia,
		/// <summary>WI</summary>
		Wisconsin,
		/// <summary>WY</summary>
		Wyoming
	}
 
	#endregion
            
	/// <summary>
	/// 2 letter state codes for the US states
	/// </summary>
	#region enum StateCode
	public enum StateCode
	{
		/// <summary>Alabama</summary>
		AL = State.Alabama,
		/// <summary>Alaska</summary>
		AK = State.Alaska,
		/// <summary>American Samoa</summary>
		AS = State.American_Samoa,
		/// <summary>Arizona</summary>
		AZ = State.Arizona,
		/// <summary>Arkansas</summary>
		AR = State.Arkansas,
		/// <summary>California</summary>
		CA = State.California,
		/// <summary>Colorado</summary>
		CO = State.Colorado,
		/// <summary>Connecticut</summary>
		CT = State.Connecticut,
		/// <summary>Delaware</summary>
		DE = State.Delaware,
		/// <summary>District of Columbia</summary>
		DC = State.District_Of_Columbia,
		/// <summary>Federated States of Micronesia</summary>
		FM = State.Federated_States_Of_Micronesia,
		/// <summary>Florida</summary>
		FL = State.Florida,
		/// <summary>Georgia</summary>
		GA = State.Georgia,
		/// <summary>Guam</summary>
		GU = State.Guam,
		/// <summary>Hawaii</summary>
		HI = State.Hawaii,
		/// <summary>Idaho</summary>
		ID = State.Idaho,
		/// <summary>Illinois</summary>
		IL = State.Illinois,
		/// <summary>Indiana</summary>
		IN = State.Indiana,
		/// <summary>Iowa</summary>
		IA = State.Iowa,
		/// <summary>Kansas</summary>
		KS = State.Kansas,
		/// <summary>Kentucky</summary>
		KY = State.Kentucky,
		/// <summary>Louisiana</summary>
		LA = State.Louisiana,
		/// <summary>Maine</summary>
		ME = State.Maine,
		/// <summary>Marshall Islands</summary>
		MH = State.Marshall_Islands,
		/// <summary>Maryland</summary>
		MD = State.Maryland,
		/// <summary>Massachusetts</summary>
		MA = State.Massachusetts,
		/// <summary>Michigan</summary>
		MI = State.Michigan,
		/// <summary>Minnesota</summary>
		MN = State.Minnesota,
		/// <summary>Mississippi</summary>
		MS = State.Mississippi,
		/// <summary>Missouri</summary>
		MO = State.Missouri,
		/// <summary>Montana</summary>
		MT = State.Montana,
		/// <summary>Nebraska</summary>
		NE = State.Nebraska,
		/// <summary>Nevada</summary>
		NV = State.Nevada,	
		/// <summary>New Hampshire</summary>
		NH = State.New_Hampshire,
		/// <summary>New Jersey</summary>
		NJ = State.New_Jersey,
		/// <summary>New Mexico</summary>
		NM = State.New_Mexico,
		/// <summary>New York</summary>
		NY = State.New_York,
		/// <summary>North Carolina</summary>
		NC = State.North_Carolina,
		/// <summary>North Dakota</summary>
		ND = State.North_Dakota,
		/// <summary>Northern Mariana Islands</summary>
		MP = State.Northern_Mariana_Islands,
		/// <summary>Ohio</summary>
		OH = State.Ohio,
		/// <summary>Oklahoma</summary>
		OK = State.Oklahoma,
		/// <summary>Oregon</summary>
		OR = State.Oregon,
		/// <summary>Palau</summary>
		PW = State.Palau,
		/// <summary>Pennsylvania</summary>
		PA = State.Pennsylvania,
		/// <summary>Puerto Rico</summary>
		PR = State.Puerto_Rico,
		/// <summary>Rhode Island</summary>
		RI = State.Rhode_Island,
		/// <summary>South Carolina</summary>
		SC = State.South_Carolina,
		/// <summary>South Dakota</summary>
		SD = State.South_Dakota,
		/// <summary>Tennessee</summary>
		TN = State.Tennessee,
		/// <summary>Texas</summary>
		TX = State.Texas,
		/// <summary>Utah</summary>
		UT = State.Utah,
		/// <summary>Vermont</summary>
		VT = State.Vermont,
		/// <summary>Virgin Islands</summary>
		VI = State.Virgin_Islands,
		/// <summary>Virginia</summary>
		VA = State.Virginia,
		/// <summary>Washington</summary>
		WA = State.Washington,
		/// <summary>West Virginia</summary>
		WV = State.West_Virginia,
		/// <summary>Wisconsin</summary>
		WI = State.Wisconsin,
		/// <summary>Wyoming</summary>
		WY = State.Wyoming
	}
	#endregion
}
