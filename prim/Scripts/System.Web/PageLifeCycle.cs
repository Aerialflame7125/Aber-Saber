internal enum PageLifeCycle
{
	Unknown = 1,
	Start,
	PreInit,
	Init,
	InitComplete,
	PreLoad,
	Load,
	ControlEvents,
	LoadComplete,
	PreRender,
	PreRenderComplete,
	SaveStateComplete,
	Render,
	Unload,
	End
}
